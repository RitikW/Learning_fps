using UnityEngine;
using UnityEngine.UI;
public class AmmoUI : MonoBehaviour
{
    public Text r_ammoUI;
    public Text m_ammoUI;
    // Start is called before the first frame update

    // Update is called once per frame
   
        public void SetMaxAmmo(int ammo)
        {
            m_ammoUI.text = ammo.ToString();
        }

        public void SetAmmo(int ammo)
        {
           r_ammoUI.text = ammo.ToString();
        }
    
}
