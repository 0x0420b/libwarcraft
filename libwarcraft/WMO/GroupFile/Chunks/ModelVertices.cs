//
//  ModelVertices.cs
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

using System.Collections.Generic;
using System.IO;
using Warcraft.Core;
using Warcraft.Core.Interfaces;
using Warcraft.Core.Structures;

namespace Warcraft.WMO.GroupFile.Chunks
{
	/// <summary>
	/// The vertices of the model.
	///
	/// The vertices are stored in a Z-up, -Y-forward system in the files. Internally, this is converted
	/// to Y-up, which OpenGL traditionally uses. When developing, use Y-up with libwarcraft - the vertices are
	/// automatically converted to WoW's system when serializing.
	/// </summary>
	public class ModelVertices : IIFFChunk, IBinarySerializable
	{
		public const string Signature = "MOVT";

		public readonly List<Vector3f> Vertices = new List<Vector3f>();

		public ModelVertices()
		{
		}

		public ModelVertices(byte[] inData)
		{
			LoadBinaryData(inData);
		}

		public void LoadBinaryData(byte[] inData)
		{
			using (MemoryStream ms = new MemoryStream(inData))
            {
            	using (BinaryReader br = new BinaryReader(ms))
            	{
		            while (ms.Position < ms.Length)
		            {
			            this.Vertices.Add(br.ReadVector3f());
		            }
            	}
            }
		}

		public string GetSignature()
		{
			return Signature;
		}

		public byte[] Serialize()
		{
			using (MemoryStream ms = new MemoryStream())
            {
            	using (BinaryWriter bw = new BinaryWriter(ms))
            	{
		            foreach (Vector3f vertex in this.Vertices)
		            {
			            bw.WriteVector3f(vertex);
		            }
            	}

            	return ms.ToArray();
            }
		}
	}
}

