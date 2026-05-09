<template>
  <div class="admin-page generic-table-page">
    <div class="page-header">
      <div>
        <div class="page-title">{{ loc('AdminPanel_Arena') }}</div>
        <div class="page-subtitle">{{ loc('AdminPanel_ArenaSubtitle') }}</div>
      </div>
      <button class="pill" @click="requestData">{{ loc('AdminPanel_Refresh') }}</button>
    </div>

    <AdminTableShell
      :columns="columns"
      :items="arenaSessions"
      :loading="arenaLoading"
      :loading-text="loc('AdminPanel_Loading')"
      :empty-text="loc('AdminPanel_NoArenaSessionsFound')"
    >
      <template #default="{ item }">
        <div class="cell strong">#{{ item.id }}</div>
        <div class="cell">{{ item.title }}</div>
        <div class="cell muted">{{ item.type }}</div>
        <div class="cell">{{ item.owner }}</div>
        <div class="cell id">{{ item.players }}/{{ item.maxPlayers }}</div>
        <div class="cell muted">{{ item.status }}</div>
      </template>
    </AdminTableShell>
  </div>
</template>

<script>
import { mapGetters, mapMutations, mapState } from 'vuex'
import AdminTableShell from '../components/AdminTableShell'

export default {
  name: 'Arena',
  components: { AdminTableShell },
  computed: {
    ...mapState('reportMenu', ['arenaSessions', 'arenaLoading']),
    ...mapGetters('localization', ['loc']),
    columns: function () {
      return [
        { key: 'id', label: this.loc('AdminPanel_Id'), width: '.45fr' },
        { key: 'title', label: this.loc('AdminPanel_Map'), width: '1.2fr' },
        { key: 'type', label: this.loc('AdminPanel_Type'), width: '1fr' },
        { key: 'owner', label: this.loc('AdminPanel_Owner'), width: '1.1fr' },
        { key: 'players', label: this.loc('AdminPanel_Players'), width: '.75fr' },
        { key: 'status', label: this.loc('AdminPanel_Status'), width: '.85fr' }
      ]
    }
  },
  mounted: function () {
    this.requestData()
  },
  methods: {
    ...mapMutations('reportMenu', ['startArenaRequest']),
    requestData: function () {
      this.startArenaRequest()
      window.mp.triggerServer('admin:arena:request')
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
.cell {
  min-width: 0;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  &.strong {
    font-weight: 700;
  }
  &.muted {
    color: rgba(255, 255, 255, 0.64);
  }
  &.id {
    color: rgba(255, 255, 255, 0.62);
    font-weight: 700;
    font-size: clamp(1.02rem, 1.12vw, 1.2rem);
  }
}
</style>
