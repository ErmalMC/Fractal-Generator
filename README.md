# Fractal-Generator

Windows Forms Project by: Ermal Baki and Aleksandra Spasenovska

# Description

A Windows Forms application that genertaes and vizualises fractal images from a variety of mathematical sets. Supports the: Mandelbrot Set, Multibrot Set, Quadratic Julia Set, Polynomial Julia Set, Burning Ship Julia Set, Burning Ship Set, Nova Set, Phoenix Set, Newtonian Set and Rational Map Set. Features an intuitive interface, an options menu to set generation variables, customizable color palettes, and the ability to save generated fractals as image files.

## Data Structures and methods

The main access form is ``` public partial class Main : Form ``` which is used to select the a specific set and open a new form for the given set. All classes and methods include a commented summary of their given function.

The list of mathematical set classes is:

```public partial class Quadratic_Julia_Set : Form```

```public partial class Polynomial_Julia_Set : Form```

```public partial class Burning_Ship_Julia_Set : Form```

```public partial class Phoenix_Fractal : Form```

```public partial class Nova : Form```

```public partial class Mandelbrot_Set : Form```

```public partial class Multibrot_Set : Form```

```public partial class Burning_Ship_Fractal : Form```

```public partial class Rational_Map : Form```

```public partial class Newtonian_Fractal : Form```

Each partial class for a given set includes access to the options menu and a local color palette to be used during the generation. A universal options class ``` public partial class Options : Form ``` is used to store and set the variables for each set seperately. 

The set form includes the ability to save the generated fractal as a: JPG, PNG, TIFF, GIF or BMP file using the function ```private void saveAsToolStripMenuItem_Click``` .

```csharp
private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
{
    // Set the filter for the Save File dialog
    dlgSaveFile.Filter = "Bitmap Image|*.bmp|JPEG Image|*.jpg;*.jpeg|GIF Image|*.gif|PNG Image|*.png|TIFF Image|*.tif;*.tiff";
    // Set the initial filter index to 4 (PNG)
    dlgSaveFile.FilterIndex = 4;
    if (dlgSaveFile.ShowDialog() == DialogResult.OK) // Display the Save File dialog
    {
        string filename = dlgSaveFile.FileName;
        string extension = filename.Substring(filename.LastIndexOf("."));
        ImageFormat imageFormat = extension switch // Determine the appropriate ImageFormat based on the file extension
        {
            ".bmp" => ImageFormat.Bmp,
            ".jpg" or ".jpeg" => ImageFormat.Jpeg,
            ".gif" => ImageFormat.Gif,
            ".png" => ImageFormat.Png,
            ".tif" or ".tiff" => ImageFormat.Tiff,
            _ => ImageFormat.Png,
        };
        bitmap.Save(filename, imageFormat); // Save the bitmap to the selected file using the determined ImageFormat
    }
}
```

# User Manual

