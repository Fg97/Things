using System.Drawing;
using System.IO;

namespace Kontur.ImageTransformer.ServiceMethods
{
	static class ImageMethods
	{
		const int startPngLength = 8, endPngLength = 8;
		static byte[] startPng = new byte[startPngLength] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
		static byte[] endPng = new byte[endPngLength] { 0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82 };

		public static Image LoadPngImage(Stream pngImageStream, long pngStreamSize, bool onlyOneImageInStream = false)
		{
			byte[] buffer = new byte[pngStreamSize];
			using(var pngInMemory = new MemoryStream(buffer))
			{
				pngImageStream.CopyTo(pngInMemory);

				int readByte = 0, index = 0;
				int startIndex = 0, endIndex = 0;

				//Начало PNG-файла
				do
				{
					if(buffer[readByte] == startPng[index])
						index++;
					else index = 0;

					readByte++;
				} while(index != startPngLength && readByte < buffer.Length);

				if(index != startPngLength)
					return null; //throw new Exception("Начало PNG-файла не найдено");

				startIndex = readByte - startPngLength;

				//Конец PNG-файла
				if(onlyOneImageInStream)
				{
					index = endPngLength - 1;
					readByte = buffer.Length - 1;
					
					do
					{
						if(buffer[readByte] == endPng[index])
							index--;
						else index = endPngLength - 1;

						readByte--;
					} while(index >= 0 && readByte >= 0);

					if(index >= 0)
						return null; //throw new Exception("Конец PNG-файла не найден");

					endIndex = readByte + endPngLength;
				}
				else
				{
					index = 0;

					do
					{
						if(buffer[readByte] == endPng[index])
							index++;
						else index = 0;

						readByte++;
					} while(index != endPngLength && readByte < buffer.Length);

					if(index != endPngLength)
						return null; //throw new Exception("Конец PNG-файла не найден");

					endIndex = readByte - 1;
				}

				using(var pngImage = new MemoryStream(buffer, startIndex, endIndex - startIndex + 1))
					return Image.FromStream(pngImage, false, false);
			}
		}
	}
}