<template>
  <div class="staff-chat">
    <div class="staff-chat__title">{{ loc('AdminPanel_StaffChat') }}</div>
    <div class="staff-chat__messages scroll">
      <div
        v-for="item in normalizedMessages"
        :key="item.id"
        class="staff-chat__message"
      >
        <div class="staff-chat__message-header">
          <span class="name">{{ item.displayName }}</span>
          <span class="meta">{{ item.displayMeta }}</span>
        </div>
        <div class="text">{{ item.text }}</div>
      </div>
      <div class="staff-chat__empty" v-if="staffChatMessages.length === 0">
        {{ loc('AdminPanel_NoStaffMessages') }}
      </div>
    </div>
    <div class="staff-chat__input-wrap">
      <input
        v-model.trim="message"
        :placeholder="loc('AdminPanel_StaffChatPlaceholder')"
        @keydown.enter="sendMessage"
      >
      <button class="send" @click="sendMessage"></button>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapMutations, mapState } from 'vuex'

export default {
  name: 'StaffChat',
  data: function () {
    return {
      message: ''
    }
  },
  computed: {
    ...mapState('reportMenu', ['staffChatMessages', 'mySettings']),
    ...mapGetters('localization', ['loc'])
    ,
    ownPlayerName: function () {
      return this.mySettings.name || 'Unknown'
    },
    ownPlayerId: function () {
      return this.$store.state.hud && this.$store.state.hud.id !== undefined
        ? this.$store.state.hud.id
        : null
    },
    normalizedMessages: function () {
      return this.staffChatMessages.map((item) => {
        const playerName = item.playerName || this.extractPlayerName(item.author)
        const playerId = item.playerId !== undefined && item.playerId !== null
          ? item.playerId
          : this.extractPlayerId(item.author)
        const date = item.date || this.extractMetaParts(item.meta).date
        const time = item.time || this.extractMetaParts(item.meta).time

        return {
          ...item,
          displayName: playerId !== null && playerId !== undefined && playerId !== ''
            ? `${playerName} [${playerId}]`
            : playerName,
          displayMeta: [date, time].filter(Boolean).join(' | ')
        }
      })
    }
  },
  methods: {
    ...mapMutations('reportMenu', ['addStaffChatMessage']),
    extractPlayerName: function (author) {
      if (!author) return this.ownPlayerName
      return author.replace(/\s*\[\d+\]\s*$/, '').trim()
    },
    extractPlayerId: function (author) {
      if (!author) return this.ownPlayerId
      const matchedId = author.match(/\[(\d+)\]\s*$/)
      return matchedId ? Number(matchedId[1]) : this.ownPlayerId
    },
    extractMetaParts: function (meta) {
      if (!meta) {
        return { date: '', time: '' }
      }

      const [date = '', time = ''] = meta.split('|').map((part) => part.trim())
      return { date, time }
    },
    getCurrentStamp: function () {
      const now = new Date()
      return {
        date: now.toLocaleDateString('en-GB'),
        time: now.toLocaleTimeString('en-GB', { hour: '2-digit', minute: '2-digit' })
      }
    },
    sendMessage: function () {
      if (!this.message) return
      const stamp = this.getCurrentStamp()
      this.addStaffChatMessage({
        playerName: this.ownPlayerName,
        playerId: this.ownPlayerId,
        date: stamp.date,
        time: stamp.time,
        text: this.message
      })
      this.message = ''
    }
  }
}
</script>

<style lang="scss" scoped>
.staff-chat {
  width: 17.5vw;
  min-width: 18rem;
  max-width: 22rem;
  height: 100%;
  background: rgba(7, 11, 23, 0.65);
  border-left: 1px solid rgba(255, 255, 255, 0.15);
  display: flex;
  flex-flow: column;
  &__title {
    padding: 2.2rem 1.45rem 1.25rem;
    font-size: clamp(1.6rem, 2vw, 2.2rem);
    font-weight: 700;
    letter-spacing: .04em;
  }
  &__messages {
    flex: 1;
    overflow-y: auto;
    padding: .35rem .9rem .45rem 1rem;
  }
  &__message {
    margin-bottom: .85rem;
    &-header {
      display: flex;
      align-items: baseline;
      gap: .5rem;
      .name {
        color: #d7ff3f;
        font-weight: 700;
        font-size: clamp(.82rem, .92vw, 1rem);
      }
      .meta {
        color: rgba(255,255,255,.45);
        font-size: clamp(.63rem, .72vw, .76rem);
      }
    }
    .text {
      margin-top: .1rem;
      text-transform: none;
      font-size: clamp(.82rem, .9vw, .95rem);
      color: rgba(255,255,255,.9);
      line-height: 1.2;
    }
  }
  &__empty {
    font-size: clamp(.78rem, .9vw, .9rem);
    color: rgba(255, 255, 255, 0.45);
    padding: .8rem 0;
  }
  &__input-wrap {
    min-height: 4.8rem;
    padding: .75rem .9rem;
    border-top: 1px solid rgba(255, 255, 255, 0.12);
    display: flex;
    align-items: center;
    gap: .8rem;
    input {
      flex: 1;
      height: 3rem;
      border: none;
      outline: none;
      border-radius: 2rem;
      background: rgba(0, 0, 0, 0.45);
      color: rgba(255,255,255,.95);
      font-size: clamp(.82rem, .92vw, .95rem);
      padding: 0 1.5rem;
      text-transform: none;
    }
    .send {
      width: 2.7rem;
      height: 2.7rem;
      border: none;
      border-radius: 50%;
      background: linear-gradient(135deg, rgba(92, 130, 255, 0.95), rgba(56, 92, 217, 0.95));
      box-shadow: 0 0 .95rem rgba(70, 110, 255, 0.35);
      position: relative;
      cursor: pointer;
      transition: background .18s ease, box-shadow .18s ease, filter .18s ease;
      &:before {
        content: '';
        position: absolute;
        inset: .68rem;
        background: center / contain no-repeat url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' fill='none'%3E%3Cpath d='M20.64 3.36a1.2 1.2 0 0 0-1.27-.25L4.64 8.58a1.2 1.2 0 0 0 .08 2.28l6.06 1.91 1.92 6.06a1.2 1.2 0 0 0 2.28.08l5.66-14.28a1.2 1.2 0 0 0-.01-1.27Z' fill='white'/%3E%3Cpath d='m10.98 12.99 3.28-3.28' stroke='%232E4CB8' stroke-width='1.35' stroke-linecap='round'/%3E%3C/svg%3E");
      }
      &:hover {
        filter: brightness(1.08);
        box-shadow: 0 0 1.15rem rgba(70, 110, 255, 0.48);
      }
    }
  }
}
</style>
