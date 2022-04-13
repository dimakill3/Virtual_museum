using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GuideWindowManager : MonoBehaviour
{
    // Кнопки меню гида
    [SerializeField]
    Button textGuideButton, audioGuideButton, artistLinkButton, showpieceLinkButton;
    // Панели гидов
    [SerializeField]
    GameObject textGuidePanel, audioGuidePanel;
    // Основное описание экспоната
    [SerializeField]
    TextMeshProUGUI showpieceMainDescription;
    // Вторичное описание экспоната
    [SerializeField]
    TextMeshProUGUI showpieceDescription;
    // Для вывода статуса плеера
    [SerializeField]
    TextMeshProUGUI audioGuideStatus;
    // Экспонат, для которого открыт гид
    private IShowpiece currentShowpiece;
    // Текущий аудиогид
    private AudioTell currentAudioGuide;

    private bool isTextGuidePanelActive = false;

    private bool isAudioGuidePanelActive = false;

    private DecoratorManager operationManager;

    private void Awake()
    {
        textGuideButton.interactable = false;
        audioGuideButton.interactable = false;
    }

    // Подписка на события
    private void OnEnable()
    {
        MenuManager.menuManager.OnGuideOpen += SetOpenInterface;
        MenuManager.menuManager.OnGuideClose += SetCloseInterface;
    }

    // Отписка от событий
    private void OnDisable()
    {
        MenuManager.menuManager.OnGuideOpen -= SetOpenInterface;
        MenuManager.menuManager.OnGuideClose -= SetCloseInterface;
    }

    // Настроить интерфейс гида при открытии
    private void SetOpenInterface(Showpiece sp)
    {
        currentShowpiece = sp;

        Guide guide = Customer.customer.GetGuide(sp.GetHallID(), typeof(AudioTell));

        if (guide != null)
            currentAudioGuide = guide.GetAudioGuide();

        if (currentAudioGuide != null)
            StopAudioGuide();

        if (Customer.customer.FindGuide(sp.GetHallID(), typeof(TextTell)))
            textGuideButton.interactable = true;

        if (Customer.customer.FindGuide(sp.GetHallID(), typeof(AudioTell)))
            audioGuideButton.interactable = true;

        artistLinkButton.interactable = true;
        showpieceLinkButton.interactable = true;

        showpieceMainDescription.text = $"Название: {sp.GetName()}\n" +
            $"Тематика: {sp.GetTheme()}\n";

        showpieceDescription.text = $"Описание: {currentShowpiece.GetDescription()}";

        operationManager = new DecoratorManager();
    }

    // Вернуть интерфейс гида в исходное состояние при закрытии
    private void SetCloseInterface()
    {
        textGuidePanel.SetActive(false);
        isTextGuidePanelActive = false;

        audioGuidePanel.SetActive(false);
        isAudioGuidePanelActive = false;

        textGuideButton.interactable = false;

        audioGuideButton.interactable = false;
    }

    // Обработка открытия/закрытия текстового гида
    public void TextGuideButtonClick()
    {
        if (isTextGuidePanelActive)
        {
            textGuidePanel.SetActive(false);
            isTextGuidePanelActive = false;
        }
        else
        {
            textGuidePanel.SetActive(true);
            isTextGuidePanelActive = true;
        }
    }

    // Обработка открытия/закрытия аудиогида
    public void AudioGuideButtonClick()
    {
        if (isAudioGuidePanelActive)
        {
            audioGuidePanel.SetActive(false);
            isAudioGuidePanelActive = false;
        }
        else
        {
            audioGuidePanel.SetActive(true);
            isAudioGuidePanelActive = true;
        }
    }

    // Добавить к описанию экспоната ссылку на создателя
    public void ShowArtistLinkButtonClick()
    {
        //artistLinkButton.interactable = false;

        operationManager.AddDecorator(new ArtistLinkDecorator());

        //currentShowpiece = new ArtistLinkDecorator(currentShowpiece);

        //showpieceDescription.text = $"Описание: {currentShowpiece.GetDescription()}";

        showpieceDescription.text = $"Описание: {operationManager.ProcessDecorators(currentShowpiece)}";
    }

    // Добавить к описанию экспоната ссылку на экспонат
    public void ShowShowpieceLinkButtonClick()
    {
        //showpieceLinkButton.interactable = false;

        operationManager.AddDecorator(new ShowpieceLinkDecorator());

        //currentShowpiece = new ShowpieceLinkDecorator(currentShowpiece);

        //showpieceDescription.text = $"Описание: {currentShowpiece.GetDescription()}";
        showpieceDescription.text = $"Описание: {operationManager.ProcessDecorators(currentShowpiece)}";
    }

    // Обработка кнопок плеера (для аудиогида)
    public void StopAudioGuide()
    {
        audioGuideStatus.text = "Статус: " + currentAudioGuide.Stop();
    }

    public void PlayAudioGuide()
    {
        audioGuideStatus.text = "Статус: " + currentAudioGuide.Play();
    }

    public void PauseAudioGuide()
    {
        audioGuideStatus.text = "Статус: " + currentAudioGuide.Pause();
    }
}
 