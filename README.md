# 250-Capstone

1) BOpen Notepad as administrator, and use the file menu to open this file C:\Program Files\Unity\Editor\Data\Tools\mergespecfile.txt

Place these following lines at the bottom of the .txt file:

# Tortoise Git Merge
* use "%programs%\TortoiseGit\bin\TortoiseGitMerge.exe" -base:"%b" -mine:"%l" -theirs:"%r" -merged:"%d"

3) In the github folder, click view and enable hidden files. Open .git, and open config.txt Paste these following lines in this .txt:

[merge]
    tool = unityyamlmerge

    [mergetool "unityyamlmerge"]
    trustExitCode = false
    cmd = 'C:\\Program Files\\Unity\\Editor\\Data\\Tools\\UnityYAMLMerge.exe' merge -p "$BASE" "$REMOTE" "$LOCAL" "$MERGED"

4) When you're prompted with conflicting files, click to open cmd and type:

git mergetool

This will open the merge tool, TortoiseMerge, which is preinstalled on the school alienware. 

5) Right click on the conflicted lines in the left column of ToirtoiseMerge, and select "replace with this (default)"

6) File save within tortoise, and close it. The scene should now be merged. There will be several copies within the scene folder, leave these alone.
