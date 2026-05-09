<template>
  <div class="player-support">
    <div class="player-support__list-view" v-if="currentChat === null">
      <div class="table-head">
        <div>{{ loc('AdminPanel_Date') }}</div>
        <div>{{ loc('AdminPanel_Name') }}</div>
        <div>{{ loc('AdminPanel_Id') }}</div>
        <div>{{ loc('AdminPanel_Issue') }}</div>
        <div></div>
        <div></div>
      </div>
      <div class="table-body scroll">
        <div class="table-row" v-for="item in reports" :key="item.id">
          <div class="date">{{ formatDate(item.time, false) }}</div>
          <div class="name">{{ item.reporterName }}</div>
          <div class="id">{{ item.reporterId }}</div>
          <div class="issue">{{ getIssue(item) }}</div>
          <button class="action-btn open" @click="setCurrentChat(item)">{{ loc('AdminPanel_Open') }}</button>
          <button class="action-btn tp" @click="teleportToPlayer(item)">{{ loc('AdminPanel_TpToPlayer') }}</button>
        </div>
        <div class="empty-row" v-if="reports.length === 0">{{ loc('AdminPanel_NoOpenReports') }}</div>
      </div>
    </div>

    <div class="player-support__dialog-view" v-else>
      <div class="dialog-header">
        <div class="dialog-header__meta">{{ loc('AdminPanel_DialogueWith') }}</div>
        <div class="dialog-header__main">
          <span class="name">{{ currentChat.reporterName }}</span>
          <span class="id">[{{ currentChat.reporterId }}]</span>
          <span class="level">{{ loc('AdminPanel_Level') }} {{ currentChat.reporterLevel || 24 }}</span>
        </div>
        <div class="dialog-header__sub">{{ loc('AdminPanel_ReportedAgo') }}</div>
        <button class="action-btn back" @click="setCurrentChat(null)">{{ loc('AdminPanel_GoBack') }}</button>
      </div>
      <div class="dialog-messages scroll">
        <div
          class="message"
          v-for="(item, index) in currentChat.messages"
          :key="index"
          :class="{ admin: item.sender !== currentChat.reporterName }"
        >
          <div class="message__avatar">{{ getInitials(item.sender) }}</div>
          <div class="message__content">
            <div class="message__top">
              <div class="author">{{ item.sender }}</div>
              <div class="time">{{ formatDate(currentChat.time, true) }}</div>
              <div class="quick-actions" v-if="item.sender === currentChat.reporterName">
                <button @click="copyMessage(item.text)">{{ loc('AdminPanel_CopyMessage') }}</button>
                <button @click="teleportToPlayer(currentChat)">{{ loc('AdminPanel_Teleport') }}</button>
                <button @click="spectatePlayer(currentChat)">{{ loc('AdminPanel_Spectate') }}</button>
              </div>
            </div>
            <div class="text">{{ item.text }}</div>
          </div>
        </div>
      </div>
      <div class="dialog-input">
        <input
          type="text"
          :placeholder="loc('ReportMenu_22')"
          v-model="currentMessageModel"
          @keydown.enter="sendMessage"
        >
        <button class="send" @click="sendMessage"></button>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapMutations, mapState } from 'vuex'
export default {
  name: "ReportsList",

  computed: {
    ...mapState('reportMenu', ['reports', 'messageModel', 'currentChat']),
    ...mapGetters('localization', ['loc']),
    currentMessageModel: {
      get: function() {
        return this.messageModel
      },
      set: function(value) {
        this.setMessageModel(value)
      }
    }
  },

  methods: {
    ...mapMutations('reportMenu', ['setMessageModel', 'setCurrentChat']),
    formatDate: function (value, withTime) {
      const date = new Date(value)
      const formattedDate = date.toLocaleDateString('en-GB').replace(/\//g, '.')
      if (!withTime) return formattedDate
      return `${formattedDate} ${date.toLocaleTimeString('en-GB', { hour: '2-digit', minute: '2-digit' })}`
    },
    getIssue: function (report) {
      const playerMessage = report.messages.find((item) => item.sender === report.reporterName)
      return playerMessage ? playerMessage.text : this.loc('AdminPanel_NoIssue')
    },
    getInitials: function (name) {
      if (!name) return 'TT'
      return name.split(' ').map((item) => item[0]).join('').slice(0, 2).toUpperCase()
    },
    teleportToPlayer: function (report) {
      window.mp.trigger('reportMenu:hotkeys', report.id, 0)
    },
    spectatePlayer: function (report) {
      window.mp.trigger('reportMenu:hotkeys', report.id, 1)
    },
    sendMessage: function () {
      if (this.currentChat == null || !this.currentMessageModel) return
      window.mp.trigger('reportMenu:sendmessage', this.currentChat.id, this.currentMessageModel)
      this.currentMessageModel = ''
    },
    copyMessage: async function (text) {
      if (!text || !navigator.clipboard || !navigator.clipboard.writeText) return
      try {
        await navigator.clipboard.writeText(text)
      } catch (e) {
        return
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.player-support{
  width: 100%;
  height: 100%;
  padding: 2.3vh 1.2vw 1.3vh;
  font-family: Roboto, sans-serif;
  &__list-view,
  &__dialog-view {
    width: 100%;
    height: 100%;
  }
  .table-head,
  .table-row {
    display: grid;
    grid-template-columns: 1.3fr 1.2fr .45fr 2.7fr .8fr 1.2fr;
    align-items: center;
    gap: .7vw;
    text-transform: uppercase;
  }
  .table-head {
    color: rgba(255, 255, 255, 0.7);
    font-size: clamp(.78rem, .82vw, .9rem);
    padding: 0 .4vw .8vh;
    border-bottom: 1px solid rgba(255, 255, 255, 0.18);
  }
  .table-body {
    height: calc(100% - 3.7rem);
    overflow-y: auto;
    padding-right: .35vw;
  }
  .table-row {
    min-height: 5.2vh;
    font-size: clamp(.9rem, 1vw, 1.05rem);
    color: rgba(255, 255, 255, 0.86);
    border-bottom: 1px solid rgba(255, 255, 255, 0.07);
    padding: .15rem .4vw;
    .date {
      color: rgba(255,255,255,0.58);
      font-size: clamp(.72rem, .76vw, .82rem);
    }
    .name {
      font-weight: 700;
    }
    .id {
      color: rgba(255,255,255,0.65);
      font-weight: 700;
      font-size: clamp(1.1rem, 1.2vw, 1.35rem);
    }
    .issue {
      white-space: nowrap;
      overflow: hidden;
      text-overflow: ellipsis;
      text-transform: none;
    }
  }
  .action-btn {
    border: 1px solid rgba(165, 193, 255, 0.55);
    border-radius: 999px;
    background: rgba(0,0,0,0.42);
    height: 2.35rem;
    min-width: 100%;
    color: #fff;
    font-weight: 700;
    font-size: clamp(.74rem, .8vw, .88rem);
    text-transform: uppercase;
    cursor: pointer;
    &.open {
      background: rgba(62, 123, 255, 0.75);
      border-color: rgba(168, 198, 255, 0.85);
    }
    &.back {
      width: 12.8rem;
      min-width: 12.8rem;
    }
  }
  .empty-row {
    color: rgba(255, 255, 255, 0.65);
    font-size: clamp(1.2rem, 1.5vw, 1.55rem);
    padding: 2rem .4vw;
  }
  .dialog-header {
    position: relative;
    padding: .4rem 0 1rem;
    border-bottom: 1px solid rgba(255, 255, 255, 0.2);
    &__meta {
      font-size: clamp(.82rem, .88vw, .98rem);
      color: rgba(255, 255, 255, 0.55);
    }
    &__main {
      margin-top: .45rem;
      display: flex;
      align-items: baseline;
      gap: .6rem;
      .name {
        font-size: clamp(2.4rem, 2.9vw, 3.2rem);
        font-weight: 700;
      }
      .id,
      .level {
        font-size: clamp(1.15rem, 1.55vw, 1.65rem);
        color: rgba(255,255,255,0.65);
        font-weight: 700;
      }
    }
    &__sub {
      margin-top: .35rem;
      font-size: clamp(.92rem, 1vw, 1.1rem);
      color: rgba(255, 255, 255, 0.6);
    }
    .back {
      position: absolute;
      right: 0;
      top: 4rem;
    }
  }
  .dialog-messages {
    height: calc(100% - 13.3rem);
    overflow-y: auto;
    padding: .9rem 0 .5rem;
  }
  .message {
    display: flex;
    gap: .75rem;
    margin-bottom: 1rem;
    &__avatar {
      width: 2.4rem;
      height: 2.4rem;
      border-radius: 50%;
      background: #4679ff;
      color: #fff;
      font-size: .8rem;
      font-weight: 700;
      display: flex;
      align-items: center;
      justify-content: center;
      flex-shrink: 0;
    }
    &__content {
      min-width: 0;
      text-transform: none;
    }
    &__top {
      display: flex;
      align-items: center;
      gap: .5rem;
      .author {
        color: #4f8fff;
        font-size: clamp(.98rem, 1.05vw, 1.12rem);
        font-weight: 700;
        text-transform: uppercase;
      }
      .time {
        color: rgba(255,255,255,0.5);
        font-size: clamp(.74rem, .82vw, .88rem);
      }
    }
    .quick-actions {
      display: flex;
      gap: .35rem;
      button {
        border: none;
        border-radius: 999px;
        background: #4b81ff;
        color: #fff;
        font-size: clamp(.58rem, .65vw, .68rem);
        font-weight: 700;
        padding: .15rem .55rem;
        cursor: pointer;
        text-transform: uppercase;
      }
    }
    .text {
      font-size: clamp(1rem, 1.18vw, 1.22rem);
      color: rgba(255, 255, 255, 0.95);
      margin-top: .22rem;
      line-height: 1.22;
      word-break: break-word;
    }
    &.admin {
      .message__avatar {
        background: #b168ff;
      }
      .author {
        color: #c27fff;
      }
    }
  }
  .dialog-input {
    height: 4.9rem;
    border-top: 1px solid rgba(255, 255, 255, 0.16);
    display: flex;
    align-items: center;
    gap: .55rem;
    input {
      width: 100%;
      height: 3rem;
      border: none;
      outline: none;
      background: transparent;
      color: rgba(255,255,255,0.95);
      font-size: clamp(.88rem, .96vw, 1rem);
      text-transform: none;
    }
    .send {
      width: 2.45rem;
      height: 2.45rem;
      border: 1px solid rgba(255,255,255,.35);
      border-radius: 50%;
      background: transparent;
      position: relative;
      cursor: pointer;
      &:before,
      &:after {
        content: '';
        position: absolute;
        background: #fff;
      }
      &:before {
        width: 1rem;
        height: .18rem;
        top: 1.15rem;
        left: .6rem;
      }
      &:after {
        width: .42rem;
        height: .42rem;
        border-top: .18rem solid #fff;
        border-right: .18rem solid #fff;
        transform: rotate(45deg);
        top: .92rem;
        right: .6rem;
        background: transparent;
      }
    }
  }
}
</style>
