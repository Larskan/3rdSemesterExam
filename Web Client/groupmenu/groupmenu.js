

function LoadRecentGroups() {

    const node = document.getElementById("ContainerRecentGroups");

    for (i = 1; i < 7; i++)
    {
        let btn = document.createElement("button");
        btn.id = "GroupIcon";
        btn.className = "btn btn=default";
    
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
    
        btn.innerHTML = "Group Number "+i;
    
        node.appendChild(btn);
    }
}
