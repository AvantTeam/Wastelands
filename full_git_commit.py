import os

debug = False
name = input("Commit name: ").split()

if(len(name) > 1 and name[1] == "1"): debug = True
name = name[0]

print("Adding...")
os.system("git --git-dir=.gitpublic add .")
if(not debug): os.system("cls")
print("Public done")
os.system("git --git-dir=.gitprivate add .")
if(not debug): os.system("cls")
print("Added\nCommiting...")
os.system("git --git-dir=.gitpublic commit -m '{0}'".format(name))
if(not debug): os.system("cls")
print("Public done")
os.system("git --git-dir=.gitprivate commit -m '{0}'".format(name))
if(not debug): os.system("cls")
print("Commited.\nPushing...")
os.system("git --git-dir=.gitpublic push")
if(not debug): os.system("cls")
print("Public done")
os.system("git --git-dir=.gitprivate push")
if(not debug): os.system("cls")
print("Pushed")