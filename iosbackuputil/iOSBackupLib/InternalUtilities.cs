using System;
using System.IO;
using System.Text;

namespace iOSBackupLib
{
	internal static class InternalUtilities
	{
		internal static readonly byte[] MBDB_HEADER_BYTES = new byte[] { 0x6D, 0x62, 0x64, 0x62, 0x05, 0x00 };

		internal static string ReadStringValue(Stream stream)
		{
			int b0 = stream.ReadByte();
			int b1 = stream.ReadByte();

			if (b0 == 255 && b1 == 255)
				return "NA";

			ushort strLen = BitConverter.ToUInt16(new byte[] { (byte)b1, (byte)b0 }, 0);

			byte[] buffer = new byte[strLen];
			stream.Read(buffer, 0, buffer.Length);

			string decodedString = Encoding.UTF8.GetString(buffer, 0, strLen);

			return decodedString.Normalize(NormalizationForm.FormC);
		}

		internal static Int16 ReadInt16Value(Stream stream)
		{
			byte[] bBuffer = new byte[2];
			InternalUtilities.CopyStreamToBuffer(stream, ref bBuffer);
			
			return BitConverter.ToInt16(bBuffer, 0);
		}

		internal static UInt16 ReadUInt16Value(Stream stream)
		{
			byte[] bBuffer = new byte[2];
			InternalUtilities.CopyStreamToBuffer(stream, ref bBuffer);

			return BitConverter.ToUInt16(bBuffer, 0);
		}

		internal static Int32 ReadInt32Value(Stream stream)
		{
			byte[] bBuffer = new byte[4];
			InternalUtilities.CopyStreamToBuffer(stream, ref bBuffer);

			return BitConverter.ToInt32(bBuffer, 0);
		}

		internal static UInt32 ReadUInt32Value(Stream stream)
		{
			byte[] bBuffer = new byte[4];
			InternalUtilities.CopyStreamToBuffer(stream, ref bBuffer);

			return BitConverter.ToUInt32(bBuffer, 0);
		}

		internal static UInt64 ReadUInt64Value(Stream stream)
		{
			byte[] bBuffer = new byte[8];
			InternalUtilities.CopyStreamToBuffer(stream, ref bBuffer); 
			
			return BitConverter.ToUInt64(bBuffer, 0);
		}

		internal static string ReadPropertyValue(Stream stream)
		{
			byte[] length = new byte[2];
			stream.Read(length, 0, length.Length);

			if (length[0] == 255 && length[1] == 255)
				return "NA";

			ushort stringLen = BitConverter.ToUInt16(
				new byte[] { length[1], length [0] } , 0);

			byte[] bStringBuff = new byte[stringLen];
			stream.Read(bStringBuff, 0, bStringBuff.Length);

			bool foundUnprintable = false;
			for (int i = 0; i < bStringBuff.Length; i++)
			{
				if (bStringBuff[i] < 32 || bStringBuff[i] >= 128)
				{
					foundUnprintable = true;
					break;
				}
			}

			if (!foundUnprintable)
				return Encoding.UTF8.GetString(bStringBuff, 0, bStringBuff.Length);
			else
				return Convert.ToBase64String(bStringBuff);
		}

		internal static void CopyStreamToBuffer(Stream s, ref byte[] b)
		{
			s.Read(b, 0, b.Length);

			if (BitConverter.IsLittleEndian)
				Array.Reverse(b);
		}

		internal static string EpochTimeToString(int epochTime)
		{
			DateTime dtEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

			return dtEpoch.AddSeconds(epochTime).ToString("MM/dd/yyyy hh:mm:ss tt");
		}

		internal static bool ByteArraysAreEqual(byte[] bA, byte[] bB)
		{
			if (bA == null || bB == null)
				return false;
			if (bA.Length != bB.Length)
				return false;

			for (int i = 0; i < bA.Length; i++)
				if (bA[i] != bB[i])
					return false;

			return true;
		}

		

	}
}
