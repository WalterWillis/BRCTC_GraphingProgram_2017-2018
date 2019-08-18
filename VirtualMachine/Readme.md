
The Unity Project started out as a small test program and quickly escalated into a graphing utility when the need arose for analysing our data.

The Project requires an SQL database. I set up a MSSQL database on a VM for this project. Our data files along with a two programs I wrote to inject the data into the database exist in the VirtualMachine directory. The first program OriginalDataIngest worked for small datasets, but could not load in large files. I created DataBaseInjection to solve this problem. It uses a streamreader along with a specified max line size. It then parses and injects the buffer of data before reading in new lines.

The SQL files are stored in the Scripts folder and can still be used to set up an environment. However, with my current knowledge of python, the SQL and GraphingProgram are no longer necessary. The modern data analysis libraries are quite powerful. Therefore these programs are now antiquated.

All of the code written here is after approximately six months of work experience with C# and SQL. Although, my SQL experience at this point was few and far between. I used SQL for this program specifically to gain more experience using it.