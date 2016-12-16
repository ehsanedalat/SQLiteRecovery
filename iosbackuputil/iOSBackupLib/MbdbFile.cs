using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace iOSBackupLib
{
	/// <summary>
	/// 
	/// </summary>
	internal class MbdbFile 
	{
		private FileStream _fsMbdb = null;
		private bool? _validHeader = null;

        internal byte[] HeaderId;
        internal uint RecordCount;
        internal List<MbdbRecord> MbdbRecords = new List<MbdbRecord>();


        /// <summary>
        /// Initializes a new instance of the <see cref="MbdbFile"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        internal MbdbFile(string fileName)
		{
			_fsMbdb = File.OpenRead(fileName);

			if (!this.StreamHasValidHeader)
				throw new FormatException(
					"The MBDB file specified does not have a valid header." + Environment.NewLine +
					"Recieved: \"" + this.HeaderId + "\"");
		}

		/// <summary>
		/// Returns a value indicating whether a file has a valid MBDB header.
		/// </summary>
		/// <param name="sMbdb"></param>
		/// <returns></returns>
		private bool StreamHasValidHeader
		{
			get
			{
				if (_validHeader == null)
				{
					try
					{
						if (_fsMbdb.Position != 0)
							_fsMbdb.Seek(0, SeekOrigin.Begin);

						byte[] bSig = new byte[6];

						_fsMbdb.Read(bSig, 0, bSig.Length);

						this.HeaderId = bSig;

						_validHeader = InternalUtilities.ByteArraysAreEqual(this.HeaderId, InternalUtilities.MBDB_HEADER_BYTES);
					}
					catch { return false; }
				}

				return (_validHeader ?? false);
			}
		}

        /// <summary>
        /// Reads the file, and closes the stream related to it.
        /// </summary>
        internal void ReadFile()
		{
			while (_fsMbdb.Position < _fsMbdb.Length)
			{
				MbdbRecord mbdbRec = new MbdbRecord();
				mbdbRec.Domain = InternalUtilities.ReadStringValue(_fsMbdb);
				mbdbRec.path = InternalUtilities.ReadStringValue(_fsMbdb);
				mbdbRec.LinkTarget = InternalUtilities.ReadStringValue(_fsMbdb);
				mbdbRec.DataHash = InternalUtilities.ReadStringValue(_fsMbdb);
				mbdbRec.Unknown_I = InternalUtilities.ReadStringValue(_fsMbdb);
				mbdbRec.Mode = InternalUtilities.ReadUInt16Value(_fsMbdb);
				mbdbRec.iNodeLookup = InternalUtilities.ReadUInt64Value(_fsMbdb);
				mbdbRec.UserId = InternalUtilities.ReadUInt32Value(_fsMbdb);
				mbdbRec.GroupId = InternalUtilities.ReadUInt32Value(_fsMbdb);
				mbdbRec.LastModifiedTime = InternalUtilities.ReadUInt32Value(_fsMbdb);
				mbdbRec.LastAccessTime = InternalUtilities.ReadUInt32Value(_fsMbdb);
				mbdbRec.CreationTime = InternalUtilities.ReadUInt32Value(_fsMbdb);
				mbdbRec.FileLength = InternalUtilities.ReadUInt64Value(_fsMbdb);
				mbdbRec.ProtectionClass = (byte)_fsMbdb.ReadByte();
				mbdbRec.PropertyCount = (byte)_fsMbdb.ReadByte();

				mbdbRec.Properties = new Dictionary<string, string>();

        for (int i = 0; i < mbdbRec.PropertyCount; i++)
				{
					string propName = InternalUtilities.ReadStringValue(_fsMbdb);
					string propVal = InternalUtilities.ReadPropertyValue(_fsMbdb);
					mbdbRec.Properties.Add(propName, propVal);
				}

				this.MbdbRecords.Add(mbdbRec);
			}

			_fsMbdb.Close();
		}

        /// <summary>
        /// Gets the unique domains.
        /// </summary>
        /// <value>The unique domains.</value>
        internal List<string> UniqueDomains
		{
			get
			{
				var lstDomains = new List<string>();

				foreach (var mbdbRec in this.MbdbRecords)
				{
					if (lstDomains.Contains(mbdbRec.Domain))
						continue;

					lstDomains.Add(mbdbRec.Domain);
				}

				return lstDomains;
			}
		}

	}
}
