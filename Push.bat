@echo off
set /p id=Commit Name: 
git --git-dir=.gitpublic add .
echo Added
git --git-dir=.gitpublic commit -m "%id%"
echo Commiting
git --git-dir=.gitpublic push
echo Pushed