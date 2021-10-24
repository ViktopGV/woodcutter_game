mergeInto(LibraryManager.library, {

  YandexPlatformCheck: function() {
    return platformCheck();
  },

  ShowFullScreenAds: function()
  {
    showFullScreenAds();
  },

  ShowRewardAds: function()
  {
    showRewardAds();
  },

  SetData: function(key, value) {
    setData(Pointer_stringify(key), value);
  },

  GetData: function(key) {
    return getData(Pointer_stringify(key));
  },
  
  AppleDeviceCheck: function(){
    return isAppleDevice();
  }
  
});