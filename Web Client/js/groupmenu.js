var URLRoot = 'https://localhost:7002/'

/*TODO: Implement a sorting and search algorithm for a twofold of reasons: 
sorting will allow searching which is necessary for the actions at hand
the loading is async and puts the results in rando order
*/


function LoadRecentGroups() {

    const node = document.getElementById("ContainerRecentGroups");

    //TODO: make algorithm to figure out recent groups, as well as load only the recent groups
    loadGroup('1',node)
}

function LoadAllGroups() {

    const node = document.getElementById("ContainerAllGroups");

    //TODO: make algorithm to load all groups belonging to this user, and only those groups
    loadGroup('1',node)
    loadGroup('2',node)
    loadGroup('3',node)

}

/** loads a group based on id and target node.
 *  
 *  Target node is the node that the group element gets appended to
 */
async function loadGroup(id,targetNode)
{
    GroupSetter('tblGroups',id,'fldGroupName',targetNode)

}

async function GroupSetter(table, id, field, targetNode)
{
    url = URLRoot+table+'/'+id;

    await fetch(url)
    .then((response)=> response.json())
    .then((data) => CreateGroupElement(data[field],targetNode))
    
}

function CreateGroupElement(name,node)
{
    let btn = document.createElement("button");
        btn.id = "GroupIcon";
        btn.className = "btn btn=default";
    
         btn.innerHTML = name;
    
        node.appendChild(btn);
}
