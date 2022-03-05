# TripleDes_Windows
 
## Overview: 
Simple console application to convert a file into Triple-DES encryption or decrypt a Triple-DES file. 

## Usage:
- Save a file called Credentials.json at the project source root. This file is set to copy always to bin. 
- Populate the Credentials.json file like below. Ensure the byte counts are 24 and 8 as shown. Of course 
  change the values from the defaults.

      {
        "DesKey": "1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24",
        "InitVector": "1, 2, 3, 4, 5, 6, 7, 8"
      }
      
- Build the solution. 
- Place the file you want encrypted or decrypted in the same directory as the compiled exe.
- Open command prompt from the same directory as the executable. 

- To Encrypt enter: "TripleDesConsole.exe ENCRYPT inFile.extension outFile.extension". The output encrypted  
  file will be placed in the same folder as the executable. inFile.extension is not encrypted and outFile.extension
  is encrypted. 
  
- To Decrypt enter: "TripleDesConsole.exe DECRYPT inFile.extension outFile.extension". The output decrypted  
  file will be placed in the same folder as the executable. inFile.extension is encrypted and outFile.extension
  is not encrypted. 
