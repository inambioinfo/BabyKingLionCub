# BabyKingLionCub
Software to analyze similarity between two mass spectra and generate pairs of spectra

This program uses data from the database created in Meerkat v2 - Original Version to create pairs of spectra and store them as pairs
back into the "Pairs" tables of that database.  Several filters were used to reduce the number of pairs that were stored back to the
database.  These filters included a filter for matching the charge, a filter for making sure that the spectra in the pair came 
from different organisms, a filter to compare the length of the peptides, and a filter to check the beginning 4 amino acids and the 
last 4 amino acids to make sure that at least either the beginning or end sets matched to eliminate pairs that would most likely not
pass the edit distance cutoff of 2. 

####To Run:

To run this program, open a new windows command prompt and execute the following command(Do not include the ".db3" on your database name):
  
    EditDistanceFinder.exe -r PathToWhereTheDatabaseFromMeerkatOriginalVersionIsStored
  
  
 
