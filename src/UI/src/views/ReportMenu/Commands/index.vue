<template>
  <div class="admin-page commands-page">
    <div class="page-header">
      <div>
        <div class="page-title">{{ loc('AdminPanel_Commands') }}</div>
        <div class="page-subtitle">{{ loc('AdminPanel_CommandsSubtitle') }}</div>
      </div>
      <button class="pill" @click="requestData">{{ loc('AdminPanel_Refresh') }}</button>
    </div>

    <div class="commands-scroll scroll">
      <div class="state" v-if="commandsLoading">{{ loc('AdminPanel_Loading') }}</div>
      <div class="state error" v-else-if="commandsError">{{ commandsError }}</div>
      <div class="state" v-else-if="!commands.length">{{ loc('AdminPanel_NoCommandsFound') }}</div>
      <div class="command-group" v-for="group in commands" :key="group.level">
        <div class="command-group__title">{{ group.title }}</div>
        <div class="command-group__level">{{ loc('AdminPanel_Level') }} {{ group.level }}</div>
        <div class="command-group__items">
          <div class="command-chip" v-for="command in group.commands" :key="command">
            /{{ command }}
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapMutations, mapState } from 'vuex'

export default {
  name: 'Commands',
  data: function () {
    return {
      requestTimer: null
    }
  },
  computed: {
    ...mapState('reportMenu', ['commands', 'commandsLoading', 'commandsError']),
    ...mapGetters('localization', ['loc'])
  },
  mounted: function () {
    this.requestData()
  },
  beforeDestroy: function () {
    this.clearRequestTimer()
  },
  methods: {
    ...mapMutations('reportMenu', ['startCommandsRequest', 'setCommandsError']),
    clearRequestTimer: function () {
      if (!this.requestTimer) return
      clearTimeout(this.requestTimer)
      this.requestTimer = null
    },
    startRequestFallback: function () {
      this.clearRequestTimer()
      this.requestTimer = setTimeout(() => {
        if (this.commandsLoading) {
          this.setCommandsError('No response from server while loading commands.')
        }
      }, 8000)
    },
    requestData: function () {
      this.startCommandsRequest()
      this.startRequestFallback()
      if (window.mp && typeof window.mp.triggerServer === 'function') {
        window.mp.triggerServer('admin:commands:request')
      } else {
        this.setCommandsError('Admin bridge is not available.')
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.admin-page {
  width: 100%;
  height: 100%;
  padding: 2.3vh 1.2vw 1.3vh;
}
.page-header {
  display: flex;
  align-items: flex-end;
  justify-content: space-between;
  gap: 1rem;
  margin-bottom: 1rem;
}
.page-title {
  font-size: clamp(1.9rem, 2.25vw, 2.45rem);
  font-weight: 700;
}
.page-subtitle {
  margin-top: .25rem;
  color: rgba(255, 255, 255, 0.58);
  font-size: clamp(.84rem, .94vw, 1rem);
}
.pill {
  height: 2.85rem;
  padding: 0 1rem;
  border: 1px solid rgba(165, 193, 255, 0.45);
  border-radius: 999px;
  background: rgba(0, 0, 0, 0.42);
  color: #fff;
  font-size: .78rem;
  font-weight: 700;
  text-transform: uppercase;
  cursor: pointer;
}
.commands-scroll {
  height: calc(100% - 4.5rem);
  overflow-y: auto;
  padding-right: .4vw;
}
.state {
  color: rgba(255, 255, 255, 0.6);
  font-size: clamp(1rem, 1.2vw, 1.2rem);
  padding: 2rem 0;
  &.error {
    color: rgba(255, 120, 120, 0.9);
  }
}
.command-group {
  padding: 1rem 0 1.2rem;
  border-bottom: 1px solid rgba(255, 255, 255, 0.08);
  &__title {
    font-size: clamp(1.18rem, 1.35vw, 1.45rem);
    font-weight: 700;
  }
  &__level {
    margin-top: .2rem;
    color: rgba(255, 255, 255, 0.55);
    font-size: .82rem;
  }
  &__items {
    display: flex;
    flex-wrap: wrap;
    gap: .55rem;
    margin-top: .9rem;
  }
}
.command-chip {
  padding: .55rem .85rem;
  border-radius: 999px;
  background: rgba(255, 255, 255, 0.07);
  border: 1px solid rgba(255, 255, 255, 0.12);
  color: rgba(255, 255, 255, 0.92);
  font-size: .76rem;
  font-weight: 700;
  text-transform: none;
}
</style>
