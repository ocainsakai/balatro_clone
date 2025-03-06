using UnityEngine;
using UnityEngine.UI;

public class RunSelectionUI : MonoBehaviour
{
    [SerializeField] private Button newRun;
    [SerializeField] private Button continueRun;
    [SerializeField] private Button challengeRun;
    [SerializeField] private Button playBtn;
    [SerializeField] private Button backBtn;
    [SerializeField] private GameObject stakeSelection;
    [SerializeField] private GameObject deckSelection;

    void Start()
    {
        newRun.onClick.AddListener(() => UpdateNewRunUI());
        continueRun.onClick.AddListener(() => UpdateContinueUI());
        challengeRun.onClick.AddListener(() => UpdateChallengeUI());
        backBtn.onClick.AddListener(() => gameObject.SetActive(false));
    }

    private void OnEnable()
    {
        // Đảm bảo UI được cập nhật đúng khi panel được hiển thị
        newRun.onClick?.Invoke();
    }

    private void OnDisable()
    {
        // Xóa tất cả các listener khi panel bị tắt
        ClearPlayButtonListeners();
    }

    private void UpdateNewRunUI()
    {
        ShowBody();
        ClearPlayButtonListeners();
        playBtn.onClick.AddListener(() => GameController.Instance.NewRun());
    }

    private void UpdateContinueUI()
    {
        HideBody();
        ClearPlayButtonListeners();
        playBtn.onClick.AddListener(() => GameController.Instance.ContinueRun());
    }

    private void UpdateChallengeUI()
    {
        HideBody();
        ClearPlayButtonListeners();
        // Bỏ comment nếu bạn đã có phương thức ChallengeRun
        // playBtn.onClick.AddListener(() => GameController.Instance.ChallengeRun());
    }

    private void ClearPlayButtonListeners()
    {
        // Xóa tất cả các listener hiện tại của playBtn
        playBtn.onClick.RemoveAllListeners();
    }

    private void ShowBody()
    {
        // Bỏ comment nếu bạn cần hiển thị stakeSelection
        // stakeSelection.SetActive(true);
        deckSelection.SetActive(true);
    }

    private void HideBody()
    {
        // Bỏ comment nếu bạn cần ẩn stakeSelection
        // stakeSelection.SetActive(false);
        deckSelection.SetActive(false);
    }
}