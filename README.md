# Fractal-Generator

Windows Forms Project by: Ermal Baki and Aleksandra Spasenovska

# Description

A Windows Forms application that genertaes and vizualises fractal images from a variety of mathematical sets. Supports the: Mandelbrot Set, Multibrot Set, Quadratic Julia Set, Polynomial Julia Set, Burning Ship Julia Set, Burning Ship Set, Nova Set, Phoenix Set, Newtonian Set and Rational Map Set. Features an intuitive interface, an options menu to set generation variables, customizable color palettes, and the ability to save generated fractals as image files.

# Presentation of data structures and methods

## Data Structures 

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

A universal options class ```public partial class Options : Form``` is used to store and set the variables for each set seperately. 
