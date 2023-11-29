using GroupDocs.Metadata.Standards.Exif;
using GroupDocs.Metadata;

class Program
{
    static void Main()
    {
        // Получение путя к папке и получение всех изображений внутри
        string folderPath = @".\img";
        string[] files = Directory.GetFiles(folderPath);

        //Проверяем каждое изображение
        foreach (string file in files)
        {
            IsPhotoEdited(file);
        }
    }
    static bool IsPhotoEdited(string filePath)
    {
        // Используем библиотеку GroupDocs.Metadata для работы с метаданными изображения
        using (Metadata metadata = new Metadata(filePath))
        {
            // Получаем корневой пакет (IExif) из метаданных изображения
            IExif root = metadata.GetRootPackage() as IExif;

            // Если корневой пакет или его ExifPackage равны null, то изображение считается измененным
            if (root == null || root.ExifPackage == null)
            {
                Console.WriteLine("Image edited: " + filePath + "\n");
                return true;
            }

            // Если корневой пакет и его ExifPackage не равны null, то изображение считается неизмененным
            else
            {
                Console.WriteLine("Image is not edited: " + filePath + "\n");
                return false;
            }
        }
    }

}