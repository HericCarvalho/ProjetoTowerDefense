using UnityEngine;

public class BuildNode : MonoBehaviour
{
    public Color hoverColor;

    private Renderer rend;
    private Color startColor;

    public GameObject tower;

    public int towerCost = 50;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void OnMouseDown()
    {
        if (tower != null)
        {
            Debug.Log("Já existe uma torre aqui!");
            return;
        }

        if (!PlayerEconomy.instance.SpendMoney(towerCost))
        {
            Debug.Log("Dinheiro insuficiente!");
            return;
        }

        BuildTower();
    }

    public void BuildTower()
    {
        GameObject towerToBuild = BuildManager.instance.GetTowerToBuild();

        tower = Instantiate(towerToBuild, transform.position, Quaternion.identity);
    }

    void OnMouseEnter()
    {
        if (tower != null)
            return;

        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}