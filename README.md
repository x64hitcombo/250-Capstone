# 250-Capstone

1) Before attempting to merge, place these following lines inside C:\Program Files\Unity\Editor\Data\Tools\mergespecfile.txt

# Tortoise Git Merge
* use "%programs%\TortoiseGit\bin\TortoiseGitMerge.exe" -base:"%b" -mine:"%l" -theirs:"%r" -merged:"%d"

If it doesn't save, open notepad as administrator and then open the .txt file

2) When you're prompted with conflicting files, click to open cmd and type:

git mergetool

This will open the merge tool, TortoiseMerge, which is preinstalled on the school alienware. 

3) Right click on the conflicted lines in the left column of ToirtoiseMerge, and select replace with this (default)

4) File save within tortoise, and close it. The scene should now be merged. There will be several copies within the scene folder, leave these alone. - Resolve (LocalMaster)\Assets\rInventoryManager\ItemDatabase.asset
