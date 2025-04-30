using UnityEngine;

public class ExperienceGiver : MonoBehaviour
{
    ExperienceManager experienceManager;
    
    [SerializeField] float expPoints;

    private void Start()
    {
        experienceManager = ExperienceManager.Instance;
    }

    public void GiveExperience()
    {
        experienceManager.GainExp(expPoints);
    }

    public void SetExpPoints(float amount)
    {
        expPoints = amount;
    }
}
