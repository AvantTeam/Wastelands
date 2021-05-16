@echo off
set /p id=Commit Name: 
set repos=.gitpublic .gitprivate
(for %%a in (%repos%) do ( 
	echo Adding %%a
	git --git-dir=%%a add .
	echo Added %%a
	echo Commiting %%a
	git --git-dir=%%a commit -m "%id%"
	echo Commited %%a
	echo Pushing %%a
	git --git-dir=%%a push
	echo Pushed %%a
))