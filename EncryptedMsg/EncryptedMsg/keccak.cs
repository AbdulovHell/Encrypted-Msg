using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptedMsg
{
    class KeccakSum
    {
        //    private byte[] {set; get }
        //private byte[] {set; get }

        public KeccakSum(byte[] A)
        {
            input = A;
            output = new byte[512];
            state = new byte[200];

        }
        byte[] state;
        byte[] input;
        byte[] output;

        public byte[] Input { get => input; set => input = value; }
        public byte[] Output { get => output; set => output = value; }

        public void FIPS202_SHA3_512()
        {
            Keccak(576, 1024, (ulong)input.Length, 0x06, 64);
        }

        public void FIPS202_SHA3_256()
        {
            Keccak(1088, 512, (ulong)input.Length, 0x06, 32);
        }

        UInt64 MIN(UInt64 a, UInt64 b)
        {
            return ((a) < (b) ? (a) : (b));
        }

        /** Function to store a 64-bit value using the little-endian (LE) convention.
          * On a LE platform, this could be greatly simplified using a cast.
          */
        /** Function to XOR into a 64-bit value using the little-endian (LE) convention.
          * On a LE platform, this could be greatly simplified using a cast.
          */
        UInt32 I(UInt32 x, UInt32 y)
        {
            return ((x) + 5 * (y));
        }

        void XORLane(UInt32 x, UInt32 y, UInt64 lane)
        {
            UInt32 a = sizeof(UInt64);
            UInt32 b = I(x, y);

            for (UInt32 i = a * b; i < 8 + a * b; ++i)
            {
                state[i] ^= (byte)lane;
                lane >>= 8;
            }
        }

        UInt64 ReadLane(UInt32 x, UInt32 y)
        {
            int i;
            UInt64 u = 0;

            UInt32 a = sizeof(UInt64);
            UInt32 b = I(x, y);

            for (i = 7; i >= 0; --i)
            {
                u <<= 8;
                u |= state[i + a * b];
            }
            return u;
        }

        UInt64 ROL64(UInt64 a, UInt32 offset)
        {
            return a << (int)offset ^ a >> (int)(64 - offset);
        }

        void WriteLane(UInt32 x, UInt32 y, UInt64 lane)
        {
            UInt32 i;

            UInt32 a = sizeof(UInt64);
            UInt32 b = I(x, y);


            for (i = 0; i < 8; ++i)
            {
                state[i + a * b] = (byte)lane;
                lane >>= 8;
            }
        }

        void KeccakF1600_StatePermute()
        {
            UInt32 round, x, y, j, t;
            byte LFSRstate = 0x01;

            for (round = 0; round < 24; round++)
            {
                {   /* === θ step (see [Keccak Reference, Section 2.3.2]) === */
                    UInt64 D;
                    UInt64[] C = new UInt64[5];

                    /* Compute the parity of the columns */
                    for (x = 0; x < 5; x++)
                        C[x] = ReadLane(x, 0) ^ ReadLane(x, 1) ^ ReadLane(x, 2) ^ ReadLane(x, 3) ^ ReadLane(x, 4);
                    for (x = 0; x < 5; x++)
                    {
                        /* Compute the θ effect for a given column */
                        D = C[(x + 4) % 5] ^ ROL64(C[(x + 1) % 5], 1);
                        /* Add the θ effect to the whole column */
                        for (y = 0; y < 5; y++)
                            XORLane(x, y, D);
                    }
                }

                {   /* === ρ and π steps (see [Keccak Reference, Sections 2.3.3 and 2.3.4]) === */
                    UInt64 current, temp;
                    /* Start at coordinates (1 0) */
                    x = 1; y = 0;
                    current = ReadLane(x, y);
                    /* Iterate over ((0 1)(2 3))^t * (1 0) for 0 ≤ t ≤ 23 */
                    for (t = 0; t < 24; t++)
                    {
                        /* Compute the rotation constant r = (t+1)(t+2)/2 */
                        UInt32 r = ((t + 1) * (t + 2) / 2) % 64;
                        /* Compute ((0 1)(2 3)) * (x y) */
                        UInt32 Y = (2 * x + 3 * y) % 5; x = y; y = Y;
                        /* Swap current and state(x,y), and rotate */
                        temp = ReadLane(x, y);
                        WriteLane(x, y, ROL64(current, r));
                        current = temp;
                    }
                }

                {   /* === χ step (see [Keccak Reference, Section 2.3.1]) === */
                    UInt64[] temp = new UInt64[5];
                    for (y = 0; y < 5; y++)
                    {
                        /* Take a copy of the plane */
                        for (x = 0; x < 5; x++)
                            temp[x] = ReadLane(x, y);
                        /* Compute χ on the plane */
                        for (x = 0; x < 5; x++)
                            WriteLane(x, y, temp[x] ^ ((~temp[(x + 1) % 5]) & temp[(x + 2) % 5]));
                    }
                }
                bool result;
                {   /* === ι step (see [Keccak Reference, Section 2.3.5]) === */
                    for (j = 0; j < 7; j++)
                    {
                        UInt32 bitPosition = (UInt32)((1 << (int)j) - 1); //(1 << j) - 1; /* 2^j-1 */
                        result = (LFSRstate & 0x01) != 0;
                        if ((LFSRstate & 0x80) != 0)
                            /* Primitive polynomial over GF(2): x^8+x^6+x^5+x^4+1 */
                            LFSRstate = (byte)((LFSRstate << 1) ^ 0x71);
                        else
                            LFSRstate <<= 1;
                        if (result)
                            XORLane(0, 0, (UInt32)(1 << (int)bitPosition));
                    }
                }
            }
        }

        void Keccak(UInt32 rate, UInt32 capacity, UInt64 inputByteLen, byte delimitedSuffix, UInt64 outputByteLen)
        {

            UInt32 rateInBytes = rate / 8;
            UInt32 blockSize = 0;
            UInt32 i;

            UInt64 temp = 0;

            if (((rate + capacity) != 1600) || ((rate % 8) != 0))
                return;

            /* === Initialize the state === */
            // memset(state, 0, sizeof(state));

            /* === Absorb all the input blocks === */
            while (inputByteLen > 0)
            {
                blockSize = (UInt32)MIN(inputByteLen, rateInBytes);

                for (i = 0; i < blockSize; i++)
                    state[i] ^= input[i + temp];


                //input += blockSize;
                temp += blockSize;
                inputByteLen -= blockSize;

                if (blockSize == rateInBytes)
                {
                    KeccakF1600_StatePermute();//f функция
                    blockSize = 0;
                }
            }

            /* === Do the padding and switch to the squeezing phase === */
            /* Absorb the last few bits and add the first bit of padding (which coincides with the delimiter in delimitedSuffix) */
            state[blockSize] ^= delimitedSuffix;
            /* If the first bit of padding is at position rate-1, we need a whole new block for the second bit of padding */
            if (((delimitedSuffix & 0x80) != 0) && (blockSize == (rateInBytes - 1)))
                KeccakF1600_StatePermute();
            /* Add the second bit of padding */
            state[rateInBytes - 1] ^= 0x80;
            /* Switch to the squeezing phase */
            KeccakF1600_StatePermute();

            /* === Squeeze out all the output blocks === */
            UInt32 cnt = 0;
            while (outputByteLen > 0)
            {
                blockSize = (UInt32)MIN(outputByteLen, rateInBytes);
                //state.CopyTo(output,blockSize);
                Array.Copy(state, 0, output, cnt, blockSize);
                //memcpy(output, state, blockSize);
                cnt += blockSize;
                outputByteLen -= blockSize;
                if (outputByteLen > 0)
                    KeccakF1600_StatePermute();
            }
        }
    }
}
