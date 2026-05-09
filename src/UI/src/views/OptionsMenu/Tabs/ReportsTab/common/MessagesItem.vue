<template>
  <div v-if="item.type === 1" class="messages-item admin">
    <div class="header">
      <div class="info">administrator: {{ item.name }}</div>
      <div class="time">{{ formatTime(item.time) }}</div>
    </div>
    <div class="text">{{ item.text }}</div>
  </div>
  <div v-else class="messages-item user">
    <div class="header">
      <div class="info">{{ item.name }}</div>
      <div class="time">{{ formatTime(item.time) }}</div>
    </div>
    <div class="text">{{ item.text }}</div>
  </div>
</template>

<script>
import {mapGetters} from 'vuex'

export default {
  name: 'MessagesItem',

  props: {
    item: Object
  },
  computed: {
    ...mapGetters('localization', ['loc'])
  },

  methods: {
    formatTime: function (value) {
      const date = new Date(value)
      const day = date.toLocaleDateString('ru-RU')
      const time = date.toLocaleTimeString('ru-RU')

      return `${day} ${time}`
    }
  }
}
</script>

<style lang="scss" scoped>
.messages-item {
  display: flex;
  flex-wrap: nowrap;
  white-space: pre-line;
  word-break: break-word;
  margin: 0 0 2.5rem 0;

  &.admin {
    justify-content: flex-start;

    .header {
      color: #fff;
      font-weight: 700;
      .info {
        font-size: 1.15rem;
        background: #301934 ;
        padding: 0.25rem 0.5rem;
      }

      .time {
        padding: 0.25rem 0.5rem;
      }
    }
  }

  &.user {
    justify-content: flex-end;

    .header {
      color: #fff;
      font-weight: 700;
      .info {
        font-size: 1.15rem;
        background: #00C22B;
        padding: 0.25rem 0.5rem;
      }

      .time {
        padding: 0.25rem 0.5rem;
      }
    }
  }

  .text {
    padding: 1rem;
    background: rgba(255, 255, 255, 0.05);
    text-transform: none !important;
    color: #FFFFFF;
    text-align: left;
    max-width: 30rem;
    min-width: 15rem;
  }
}
</style>
