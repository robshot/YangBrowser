# YangBrowser

To make use of the gnmi get request you have to install gNMIc on wsl.
First you have to create an ubuntu environment on your windows machine with WSL.
Make sure you have python3 installed.
After that you have to install pip: sudo apt-get install pip
When this is done you can install gnmic through this command:
bash -c "$(curl -sL https://get-gnmic.openconfig.net)"

Now you can use the GET function in the program to retrieve the value of all the leafs.
If the node isn't supported it will display an error message instead.
