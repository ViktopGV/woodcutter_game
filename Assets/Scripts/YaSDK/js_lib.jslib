mergeInto(LibraryManager.library, {

    ShowFullscreenAdv: function() {
        showFullscreenAdv();
    },

    ShowRewardAdv: function(id) {
        showRewardAdv(id);
    },

    GetDevice: function() {
        var returnStr = deviceInfo();
        var bufferSize = lengthBytesUTF8(returnStr) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(returnStr, buffer, bufferSize);
        return buffer;
    },

    SetSafeData: function(key, value) {
        key = UTF8ToString(key);
        value = UTF8ToString(value);
        setSafeData(key, value);
    },

    GetSafeData: function(key) {
        key = UTF8ToString(key);
        var returnStr = getSafeData(key);
        if(returnStr != null) {
            var bufferSize = lengthBytesUTF8(returnStr) + 1;
            var buffer = _malloc(bufferSize);
            stringToUTF8(returnStr, buffer, bufferSize);           
            return buffer;
        }
        var returnStr = "Null";
        var bufferSize = lengthBytesUTF8(returnStr) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(returnStr, buffer, bufferSize);           
        return buffer;
    },

    ItitializeLeaderboard: function(){
        initializationLeaderboard();
    },

    SetLeaderboardScore: function(leaderboard, score){
        leaderboard = UTF8ToString(leaderboard);
        setLeaderboardScore(leaderboard, score);
    },

    GetLeaderboardEntries: function(leaderboard, includeUser, quantityAround, quantityTop, avatarSize){
        leaderboard = UTF8ToString(leaderboard);
        includeUser = Boolean(includeUser);
        avatarSize = UTF8ToString(avatarSize);

        getLeaderboardEntries(leaderboard, includeUser, quantityAround, quantityTop, avatarSize);
    },

    InitializationPlayer: function(){
        initializationPlayer();
    },

    OpenAuthDialog: function(){
        openAuthDialog();
    },

    RemoveItem: function(item_key) {
        localStorage.removeItem(UTF8ToString(item_key));
    },

    IncrementStats: function(data) {
        data = JSON.parse(UTF8ToString(data));        
        incrementStats(data);
    },

    GetStats: function(key){
        getStats(UTF8ToString(key));
    },

    GetData: function() {
        getData();
    },

    SetData: function(data, flush){
        data = JSON.parse(UTF8ToString(data));
        flush = Boolean(flush);
        setData(data, flush);
    },

    IsPlayerAuth: function() {
        return player.getMode() === 'lite';
    }

    
});