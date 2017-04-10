﻿//
//  MapChunkVertexNormals.cs
//
//  Author:
//       Jarl Gullberg <jarl.gullberg@gmail.com>
//
//  Copyright (c) 2016 Jarl Gullberg
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System.IO;
using System.Collections.Generic;
using Warcraft.Core;
using Warcraft.Core.Interfaces;
using Warcraft.Core.Structures;

namespace Warcraft.ADT.Chunks.Subchunks
{
	public class MapChunkVertexNormals : IIFFChunk
	{
		public const string Signature = "MCNR";

		public List<Vector3f> HighResVertexNormals = new List<Vector3f>();
		public List<Vector3f> LowResVertexNormals = new List<Vector3f>();

		public MapChunkVertexNormals()
		{

		}

		public MapChunkVertexNormals(byte[] inData)
		{
			LoadBinaryData(inData);
		}

		public void LoadBinaryData(byte[] inData)
        {
        	using (MemoryStream ms = new MemoryStream(inData))
			{
				using (BinaryReader br = new BinaryReader(ms))
				{
					for (int y = 0; y < 16; ++y)
					{
						if (y % 2 == 0)
						{
							// Read a block of 9 high res normals
							for (int x = 0; x < 9; ++x)
							{
								sbyte X = br.ReadSByte();
								sbyte z = br.ReadSByte();
								sbyte Y = br.ReadSByte();

								this.HighResVertexNormals.Add(new Vector3f(X, Y, z));
							}
						}
						else
						{
							// Read a block of 8 low res normals
							for (int x = 0; x < 8; ++x)
							{
								sbyte X = br.ReadSByte();
								sbyte z = br.ReadSByte();
								sbyte Y = br.ReadSByte();

								this.LowResVertexNormals.Add(new Vector3f(X, Y, z));
							}
						}
					}
				}
			}
        }

        public string GetSignature()
        {
        	return Signature;
        }
	}
}

