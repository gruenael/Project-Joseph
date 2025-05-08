using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
using System.Threading.Tasks;

public class MenuController : MonoBehaviour
{

    [System.Serializable]   
    public class MusicData
    {
        public string musicTitle;
        public string musicArtist;
        public string difficulty;
        public string time;
        public float noteSpeed;
        public float noteSpawnTime;
        public TimelineAsset musicAssets;
        public AudioClip previewClip;
    }

    // Start is called before the first frame update

    [Header("Music Datas")]
    public List<MusicData> musicDatas;
    public GameObject musicRowTemplate;
    public Transform container;

    public Button playButton;
    public Button RestartButton;
    public GameObject canvasMenu;
    public AudioSource previewSource;
    public MusicData selectedMusicData = null;
    public List<GameObject> musicRows;

    private void Start()
    {
        RestartButton.onClick.AddListener(RestartGame);
        playButton.onClick.AddListener(PlayGame);
        foreach (var item in musicDatas)
        {
            GameObject row = Instantiate(musicRowTemplate,container);
            var musicData = item;
            row.GetComponent<MusicRow>().Setup(musicData);
            row.GetComponent<Button>().onClick.AddListener(()=>SelectSong(musicData));
            musicRows.Add(row);
        }
    }


    private void Update()
    {
        playButton.interactable = selectedMusicData != null;

        if(selectedMusicData != null){
            Debug.Log("Not Null");
        };
        
    }

    public void SelectSong(MusicData data)
{
    foreach (var item in musicRows) 
    { 
        if(data == item.GetComponent<MusicRow>().musicData)
        {
            item.GetComponent<CanvasGroup>().alpha = 0.9f;
            selectedMusicData = data;

            // 🔊 Play the preview
            PlayPreview(selectedMusicData);

        }
        else
        {
            item.GetComponent<CanvasGroup>().alpha = 0.7f;
        }
    }
}


    public void RestartGame(){
        FindObjectOfType<BeatManager>().noteSpawnTime = selectedMusicData.noteSpawnTime;
        FindObjectOfType<BeatManager>().noteSpeed = selectedMusicData.noteSpeed;
        FindObjectOfType<BeatManager>().musicDirector.playableAsset = selectedMusicData.musicAssets;
        FindObjectOfType<BeatManager>().StartMusic();
        Time.timeScale = 1f;
    }

    public void PlayGame()
    {   
        if (previewSource.isPlaying)
            previewSource.Stop();

        Time.timeScale = 1f;
        if (selectedMusicData.musicAssets == null)
            return;
        canvasMenu.gameObject.SetActive(false);
        FindObjectOfType<BeatManager>().noteSpawnTime = selectedMusicData.noteSpawnTime;
        FindObjectOfType<BeatManager>().noteSpeed = selectedMusicData.noteSpeed;
        FindObjectOfType<BeatManager>().musicDirector.playableAsset = selectedMusicData.musicAssets;
        FindObjectOfType<BeatManager>().StartMusic();
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlayPreview(MusicData data)
{
    if (data != null && data.previewClip != null && previewSource != null)
    {
        if (previewSource.isPlaying)
        {
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(FadeOutAndPlayNew(previewSource, data.previewClip));
        }
        else
        {
            previewSource.clip = data.previewClip;
            previewSource.Play();
        }
    }
    else
    {
        Debug.LogWarning("Missing data, preview clip, or preview source.");
    }
}


    public void StopPreview(float fadeDuration = 1f)
{
    if (previewSource != null && previewSource.isPlaying)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeOut(previewSource, fadeDuration));
    }
}

private Coroutine fadeCoroutine;

IEnumerator FadeOut(AudioSource previewSource, float duration = 1f)
{
    float startVolume = previewSource.volume;

    while (previewSource.volume > 0f)
    {
        previewSource.volume -= startVolume * Time.unscaledDeltaTime / duration;
        yield return null;
    }

    previewSource.Stop();
    previewSource.volume = startVolume; // Reset volume
}

IEnumerator FadeOutAndPlayNew(AudioSource previewSource, AudioClip newClip, float fadeDuration = 1f)
{
    float startVolume = previewSource.volume;

    while (previewSource.volume > 0f)
    {
        previewSource.volume -= startVolume * Time.unscaledDeltaTime / fadeDuration;
        yield return null;
    }

    previewSource.Stop();
    previewSource.volume = startVolume;

    previewSource.clip = newClip;
    previewSource.Play();
}

}
