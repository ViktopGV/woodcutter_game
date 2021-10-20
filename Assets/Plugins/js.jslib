mergeInto(LibraryManager.library, {

  YandexPlatformCheck: function() {
    return platformCheck();
  },

  PlatformCheck: function () {
    var Str = "";
    var bufferSize;
    var buffer;
    if(container.className == 'unity-mobile')
    {
        Str = "mobile";
        bufferSize = lengthBytesUTF8(Str) + 1;
        buffer = _malloc(bufferSize);
    }
    else    
    {
        Str = "desktop";
        bufferSize = lengthBytesUTF8(Str) + 1;
        buffer = _malloc(bufferSize);
    }
    stringToUTF8(Str, buffer, bufferSize);
    return buffer;
  },

  ShowFullScreenAds: function()
  {
    showFullScreenAds();
  },

  ShowRewardAds: function()
  {
    showRewardAds();
  }
  
});