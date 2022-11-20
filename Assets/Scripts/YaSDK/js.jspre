var s = document.createElement('script');
s.src = 'https://yandex.ru/games/sdk/v2';
s.async = true;
document.head.appendChild(s);
s.onload = function() {
YaGames
.init()
.then(ysdk => {
    window.ysdk = ysdk;
    ysdk.getStorage().then(safeStorage => Object.defineProperty(window, 'localStorage', { get: () => safeStorage }))
    showFullscreenAdv();
    
});
}
/*window.onload = (function(d) {
    console.log("START INITITETT");
    var s = d.createElement('script');
    s.src = 'https://yandex.ru/games/sdk/v2';
    s.async = true;
    d.head.appendChild(s);
    s.onload = function() {
        YaGames
        .init()
        .then(ysdk => {
            window.ysdk = ysdk;
            ysdk.getStorage().then(safeStorage => Object.defineProperty(window, 'localStorage', { get: () => safeStorage }))
            showFullscreenAdv();
            console.log("Yandex Inited")
            
        });
    }
})(document);*/

function showFullscreenAdv() {
    ysdk.adv.showFullscreenAdv({
        callbacks: {
            onOpen: function() {
                Module.SendMessage('YaGamesSDK', 'FullscreenAdvOpenedCallback');
            },
            onClose: function(wasShown) {
                Module.SendMessage('YaGamesSDK', 'FullscreenAdvClosedCallback', Number(wasShown));     
            },
            onError: function(error) {
                Module.SendMessage('YaGamesSDK', 'FullscreenAdvErrorCallback', String(error));
            },
            onOffline: function() {
                Module.SendMessage('YaGamesSDK', 'FullscreenAdvOfflineCallback');

            }
    }
})
}

function showRewardAdv(id) {
    ysdk.adv.showRewardedVideo({
        callbacks: {
            onOpen: function() {
                Module.SendMessage('YaGamesSDK', 'RewardAdvOpenCallback', id);
            },
            onRewarded: () => {
                Module.SendMessage('YaGamesSDK', 'RewardedCallback', id);
            },
            onClose: () => {
                Module.SendMessage('YaGamesSDK', 'RewardAdvClosedCallback', id);
            }, 
            onError: (e) => {
                Module.SendMessage('YaGamesSDK', 'RewardAdvErrorCallback', id, String(e));
            }
        }
    })  
}

function deviceInfo() {
    return ysdk.deviceInfo.type;
}

function setSafeData(key, value) {
    localStorage.setItem(key, value);
}

function getSafeData(key) {
    return localStorage.getItem(key);
}

function initializationLeaderboard() {
    if(typeof lb !== 'undefined') return;
    ysdk.getLeaderboards().then(_lb => window.lb = _lb);
}

function setLeaderboardScore(leaderboard, score) {
    lb.setLeaderboardScore(leaderboard, score).then(() => {
        Module.SendMessage('YaGamesSDK', 'SetLeaderboardCallback')
    });
}

function getLeaderboardEntries(leaderboard, includeUser, quantityAround, quantityTop, avatarSize) {
    lb.getLeaderboardEntries(leaderboard, { quantityTop: quantityTop, includeUser: includeUser, quantityAround: quantityAround})
      .then(res => {
        for(var i = 0; i < res.entries.length; ++i){
            res.entries[i].player.avatarSrc = res.entries[i].player.getAvatarSrc(avatarSize);
            res.entries[i].player.avatarSrcSet = res.entries[i].player.getAvatarSrcSet(avatarSize);
        }
        Module.SendMessage('YaGamesSDK', 'GetLeaderboardEntriesCallback', JSON.stringify(res));  
      });       
}

function getLeaderboardPlayerEntry(leaderboard) {
    ysdk.getLeaderboards()
    .then(lb => lb.getLeaderboardPlayerEntry(leaderboard))
    .then(res => Module.SendMessage('YaGamesSDK', 'GetLeaderboardPlayerEntryCallback', JSON.stringify(res)))
    .catch(err => {
        if (err.code === 'LEADERBOARD_PLAYER_NOT_PRESENT') {
            Module.SendMessage('YaGamesSDK', 'LeaderboardPlayerNotPresentCallback');
        }
    });
}

function initializationPlayer() {
    ysdk.getPlayer({scopes : true}).then(_player => {
    window.player = _player;
        Module.SendMessage('YaGamesSDK', 'PlayerSuccsessfullyInitedCallback', Number(_player.getMode() === 'lite'));
    });
}

function openAuthDialog() {
    ysdk.auth.openAuthDialog().then(() => {
        Module.SendMessage('YaGamesSDK', 'PlayerSuccsessfullyAuthCallback');
        initializationPlayer();
    }).catch(() => {
        Module.SendMessage('YaGamesSDK', 'PlayerRefuseAuthCallback');
    });
}

function incrementStats(data) {
    player.incrementStats(data).then(res => Module.SendMessage('YaGamesSDK', 'PlayerIncrementStatsCallback', JSON.stringify(res)))
}

function getStats(key) {
    p = [key];
    player.getStats(p).then(res => Module.SendMessage('YaGamesSDK', 'PlayerGetStatsCallback', JSON.stringify(res)));
}

function setData(data, flush) {
    player.setData(data, flush);
}

function getData() {
    player.getData().then(result => {
        Module.SendMessage('YaGamesSDK', 'PlayerGetDataCallback', JSON.stringify(result))
    })
    .catch(err => console.error(err));
}