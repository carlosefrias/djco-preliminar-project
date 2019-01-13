#pragma strict

function Start () {

}

function Update () {
	if (Input.GetKey(KeyCode.Space)) {
		Application.LoadLevel("game");
	}
}
