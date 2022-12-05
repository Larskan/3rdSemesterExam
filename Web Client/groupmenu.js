

function LoadRecentGroups() {

    const node = document.getElementById("ContainerRecentGroups");

    //window.location

    for (i = 1; i < 7; i++) //the reason this is 1 based is because it creates groups based on the counter and making it 0 based looks weird
    {
        let btn = document.createElement("button"); 
        //idk if we can make the functionality for buttons in here properly, pls figure out
        btn.id = "GroupIcon";
        btn.className = "btn btn=default";

        //Make this load groups from the DB, make it load the 6 newest groups'
        btn.innerHTML = "Recent Group Number "+i;
    


        node.appendChild(btn);
    }
}

function LoadAllGroups() {

    const node = document.getElementById("ContainerAllGroups");

    for (i = 1; i < 22; i++)
    {
        let btn = document.createElement("button");
        btn.id = "GroupIcon";
        btn.className = "btn btn=default";
    
        //Make this load groups from the DB, change the forloop to load all groups instead of fixed amount
        btn.innerHTML = "Group Number "+i;
    


        node.appendChild(btn);
    }
}
