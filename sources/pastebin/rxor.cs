        //------------------
        //Creator: aeonhack
        //Site: elitevs.net
        //Created: 9/20/2012
        //Changed: 9/20/2012
        //Version: 1.0.0
        //------------------
        public byte[] RXOR(byte[] data, byte[] key)
        {
            int N1 = 11;
            int N2 = 13;
            int NS = 257;

            for (int I = 0; I <= key.Length - 1; I++)
            {
                NS += NS % (key[I] + 1);
            }

            byte[] T = new byte[data.Length];
            for (int I = 0; I <= data.Length - 1; I++)
            {
                NS = key[I % key.Length] + NS;
                N1 = (NS + 5) * (N1 & 255) + (N1 >> 8);
                N2 = (NS + 7) * (N2 & 255) + (N2 >> 8);
                NS = ((N1 << 8) + N2) & 255;

                T[I] = (byte)(data[I] ^ (byte)(NS));
            }

            return T;
        }