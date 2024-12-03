# XML Encoding Converter

## Overview

XML Encoding Converter is a simple tool I used to convert the encoding of all XML files in a specified directory and its subdirectories to UTF-8. It can easily handle XML files with different encodings and saved with a unified UTF-8 encoding.

## How to Use

1. Prepare the XML files that need to be converted and place them in the same directory.
2. Run the tool by specifying the path of the directory in the command line. The tool will automatically search for XML files in the specified directory and all its subdirectories and perform the conversion.

Example:
```
XmlEncodingConverter <directory-path>
```

Replace `<directory-path>` with the path to the directory containing the XML files.

## Features

- **Automatic Search**: The tool automatically searches for XML files in the specified directory and all its subdirectories, so you don't need to manually specify each file.
- **Encoding Conversion**: Supports various different encodings of XML files and converts them uniformly to UTF-8.
- **No Backup Required**: During the conversion process, the original XML files are directly overwritten, making it simple and convenient.

## Notes

- This tool directly overwrites the original files, so please make sure [b]you have backed up any important data before using it[/b].
- If there are unsupported encodings in the XML files, the tool will display a warning and attempt to read using the default encoding.

## Disclaimer

This tool is provided as-is without any warranties. Use it at your own risk. The developers are not responsible for any data loss or damage that may occur as a result of using this tool.

