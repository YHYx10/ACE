<template>
  <div class="admin-page event-menu-page">
    <div class="page-header">
      <div>
        <div class="page-title">{{ loc('AdminPanel_EventMenu') }}</div>
        <div class="page-subtitle">{{ loc('AdminPanel_EventMenuSubtitle') }}</div>
      </div>
      <button class="pill" @click="requestData">{{ loc('AdminPanel_Refresh') }}</button>
    </div>

    <div class="event-layout">
      <div class="event-form">
        <div class="event-form__title">{{ loc('AdminPanel_CreateEvent') }}</div>
        <div class="event-form__grid">
          <input v-model.trim="form.name" :placeholder="loc('AdminPanel_EventName')">
          <input v-model.number="form.level" type="number" :placeholder="loc('AdminPanel_Level')">
          <input v-model.number="form.health" type="number" :placeholder="loc('AdminPanel_Health')">
          <input v-model.number="form.armor" type="number" :placeholder="loc('AdminPanel_Armor')">
          <input v-model.number="form.maxPlayers" type="number" :placeholder="loc('AdminPanel_MaxPlayers')">
          <input v-model.number="form.reward" type="number" :placeholder="loc('AdminPanel_Reward')">
          <input v-model.trim="form.portalStart" :placeholder="loc('AdminPanel_PortalStart')">
          <input v-model.trim="form.portalEnd" :placeholder="loc('AdminPanel_PortalEnd')">
        </div>
        <textarea v-model.trim="form.description" :placeholder="loc('AdminPanel_Description')"></textarea>
        <div class="event-form__actions">
          <button class="pill primary" @click="createEvent">{{ loc('AdminPanel_CreateEvent') }}</button>
        </div>
      </div>

      <div class="event-history">
        <div class="event-history__title">{{ loc('AdminPanel_RecentEvents') }}</div>
        <div class="event-history__list scroll">
          <div class="state" v-if="eventEntriesLoading">{{ loc('AdminPanel_Loading') }}</div>
          <div class="state" v-else-if="!eventEntries.length">{{ loc('AdminPanel_NoEventsFound') }}</div>
          <div class="event-card" v-for="item in eventEntries" :key="item.id || item.createdAt">
            <div class="event-card__top">
              <div class="name">{{ item.name }}</div>
              <div class="meta">{{ formatDate(item.createdAt) }}</div>
            </div>
            <div class="event-card__stats">
              <span>LVL {{ item.level }}</span>
              <span>HP {{ item.health }}</span>
              <span>AR {{ item.armor }}</span>
              <span>MAX {{ item.maxPlayers }}</span>
              <span>${{ formatMoney(item.reward) }}</span>
            </div>
            <div class="event-card__desc">{{ item.description || '-' }}</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapMutations, mapState } from 'vuex'

function defaultForm () {
  return {
    name: '',
    level: 1,
    health: 100,
    armor: 0,
    maxPlayers: 50,
    reward: 1000,
    portalStart: '',
    portalEnd: '',
    description: ''
  }
}

export default {
  name: 'EventMenu',
  data: function () {
    return {
      form: defaultForm()
    }
  },
  computed: {
    ...mapState('reportMenu', ['eventEntries', 'eventEntriesLoading']),
    ...mapGetters('localization', ['loc'])
  },
  mounted: function () {
    this.requestData()
  },
  methods: {
    ...mapMutations('reportMenu', ['startEventEntriesRequest']),
    requestData: function () {
      this.startEventEntriesRequest()
      window.mp.triggerServer('admin:events:request')
    },
    createEvent: function () {
      if (!this.form.name) return
      window.mp.triggerServer('admin:event:create', JSON.stringify(this.form))
      this.form = defaultForm()
      window.setTimeout(() => this.requestData(), 250)
    },
    formatMoney: function (value) {
      return Number(value || 0).toLocaleString('en-US')
    },
    formatDate: function (value) {
      if (!value) return '-'
      const date = new Date(value)
      if (Number.isNaN(date.getTime())) return value
      return `${date.toLocaleDateString('en-GB')} ${date.toLocaleTimeString('en-GB', { hour: '2-digit', minute: '2-digit' })}`
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
  &.primary {
    background: rgba(62, 123, 255, 0.75);
    border-color: rgba(168, 198, 255, 0.85);
  }
}
.event-layout {
  display: grid;
  grid-template-columns: 1.15fr .95fr;
  gap: 1rem;
  height: calc(100% - 4.5rem);
}
.event-form,
.event-history {
  background: rgba(5, 10, 20, 0.36);
  border: 1px solid rgba(255, 255, 255, 0.08);
  border-radius: 1rem;
  padding: 1rem;
}
.event-form {
  &__title {
    font-size: 1.2rem;
    font-weight: 700;
  }
  &__grid {
    display: grid;
    grid-template-columns: repeat(2, minmax(0, 1fr));
    gap: .7rem;
    margin-top: .9rem;
  }
  input,
  textarea {
    width: 100%;
    border: none;
    outline: none;
    border-radius: .8rem;
    background: rgba(0, 0, 0, 0.44);
    color: #fff;
    padding: 0 .95rem;
    font-size: .85rem;
    text-transform: none;
  }
  input {
    height: 2.9rem;
  }
  textarea {
    height: 8rem;
    resize: none;
    padding-top: .8rem;
    margin-top: .7rem;
  }
  &__actions {
    display: flex;
    justify-content: flex-end;
    margin-top: .8rem;
  }
}
.event-history {
  display: flex;
  flex-flow: column;
  &__title {
    font-size: 1.2rem;
    font-weight: 700;
  }
  &__list {
    flex: 1;
    overflow-y: auto;
    padding-right: .25rem;
    margin-top: .8rem;
  }
}
.state {
  color: rgba(255, 255, 255, 0.58);
  font-size: .95rem;
}
.event-card {
  padding: .85rem;
  border-radius: .85rem;
  background: rgba(255, 255, 255, 0.05);
  border: 1px solid rgba(255, 255, 255, 0.08);
  margin-bottom: .7rem;
  &__top {
    display: flex;
    justify-content: space-between;
    gap: .7rem;
    .name {
      font-size: 1rem;
      font-weight: 700;
    }
    .meta {
      color: rgba(255, 255, 255, 0.48);
      font-size: .72rem;
    }
  }
  &__stats {
    display: flex;
    flex-wrap: wrap;
    gap: .5rem;
    margin-top: .55rem;
    color: #d7ff3f;
    font-size: .72rem;
    font-weight: 700;
  }
  &__desc {
    margin-top: .55rem;
    color: rgba(255, 255, 255, 0.78);
    font-size: .82rem;
    line-height: 1.2;
    text-transform: none;
  }
}
</style>
