# What
- Tween

# How To Install

Add the lines below to `Packages/manifest.json`

- for version `1.4.0`
```csharp
"com.pancake.tween": "https://github.com/pancake-llc/tween.git?path=Assets/_Root#1.4.0",
"com.pancake.common": "https://github.com/pancake-llc/common.git?path=Assets/_Root",
"com.system-community.systemruntimecompilerservicesunsafe": "https://github.com/system-community/SystemRuntimeCompilerServicesUnsafe.git?path=Assets/_Root#4.5.3",
```

# Usages

### Delay

- `CallbackTween`, `ResetableCallbackTween`, `WaitTween` can not use `.Delay()`

```csharp
        var sequense = TweenManager.Sequence();
        sequense.Join(transform.TweenPosition(Vector3.one, 1f).OnComplete(() => Debug.Log("DONE POSITION")));
        sequense.Join(transform.TweenLocalScale(Vector3.one, 1f).OnComplete(() => Debug.Log("DONE SCALE")));
        sequense.Append(new WaitTween(5));
        sequense.Append(transform.TweenPosition(Vector3.zero, 1f).OnComplete(() => Debug.Log("DONE POSITION BACK")));
        sequense.Delay(5);
        
        sequense.Play();
```

```csharp
        transform.TweenPosition(Vector3.one, 1f).OnComplete(() => Debug.Log("DONE POSITION")).Delay(5f).Play();
```

### Loop

```csharp
var sequence = TweenManager.Sequence();
        sequence.Append(GetComponent<Image>().TweenColor(Color.red, 1f));
        sequence.SetLoops(10, ResetMode.InitialValues).OnLoop(() => Debug.Log("LOOP SEQUENSE"))
            .OnComplete(()=> Debug.Log("ON COMPLETED!!!"));
        sequence.Play();
```

- for infinite loop pass `-1` as parameter
```csharp
 GetComponent<Image>().TweenColor(Color.red, 1f).SetEase(interpolator).SetLoops(-1, ResetMode.InitialValues).OnLoop(() => Debug.Log("LOOP")).Play();
```

