using GroupDocs.Metadata.Standards.Exif;
using GroupDocs.Metadata;
using GroupDocs.Metadata.Formats.Image;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Получение путя к папке и получение всех изображений внутри
        string folderPath = @".\img";
        string[] files = System.IO.Directory.GetFiles(folderPath);

        //Проверяем каждое изображение 
        foreach (string file in files)
        {
            Console.WriteLine(file + " Фото было отредактировано: " + IsPhotoEdited(file));
        }
        Console.ReadLine();
    }
    static bool IsPhotoEdited(string filePath)
    {
        // Переменная edited используется для отслеживания того, редактирован ли изображение.
        bool edited = false;

        // Создание объекта Bitmap для представления изображения.
        Bitmap image = new Bitmap(filePath);

        // Получение массива PropertyItem, содержащего метаданные изображения.
        PropertyItem[] propItems = image.PropertyItems;

        foreach (PropertyItem item in propItems)
        {
            // Получение числового идентификатора свойства в формате short.
            short id = short.Parse(item.Id.ToString("x"), System.Globalization.NumberStyles.HexNumber);

            // Получение значения свойства в виде строки с использованием метода GetValueString.
            string value = GetValueString(item.Type, item.Value);

            switch (id)
            {
                case 0x131: // Идентификатор свойства "ImageSoftwareVersion" в формате EXIF.
                            // Проверка, начинается ли значение с "Adobe Photoshop".
                    if (value.StartsWith("Adobe Photoshop"))
                    {
                        // Изображение редактировано.
                        edited = true;

                        break;
                    }
                    break;
                default:
                    break;
            }
        }

        // Возврат значения переменной edited, указывающего на то, редактирован ли изображение.
        return edited;
    }

    static string GetValueString(short type, byte[] value)
    {
        // Логика преобразования значений в читаемый формат
        if (type == 2) // ASCII
        {
            return System.Text.Encoding.ASCII.GetString(value);
        }

        return BitConverter.ToString(value);
    }
}