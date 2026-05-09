<template>
  <div class="admin-page generic-table-page">
    <div class="page-header">
      <div>
        <div class="page-title">{{ loc('AdminPanel_Teleports') }}</div>
        <div class="page-subtitle">{{ loc('AdminPanel_TeleportsSubtitle') }}</div>
      </div>
      <button class="pill" @click="requestData">{{ loc('AdminPanel_Refresh') }}</button>
    </div>

    <AdminTableShell
      :columns="columns"
      :items="teleports"
      :loading="teleportsLoading"
      :loading-text="loc('AdminPanel_Loading')"
      :empty-text="loc('AdminPanel_NoTeleportsFound')"
    >
      <template #default="{ item }">
        <div class="cell strong">{{ item.placeName }}</div>
        <div class="cell muted">{{ item.coords }}</div>
        <div class="cell id">{{ item.dimension }}</div>
        <button class="action-btn primary" @click="go(item)">{{ loc('AdminPanel_TakeMeThere') }}</button>
      </template>
    </AdminTableShell>
  </div>
</template>

<script>
import { mapGetters, mapMutations, mapState } from 'vuex'
import AdminTableShell from '../components/AdminTableShell'

export default {
  name: 'Teleports',
  components: { AdminTableShell },
  computed: {
    ...mapState('reportMenu', ['teleports', 'teleportsLoading']),
    ...mapGetters('localization', ['loc']),
    columns: function () {
      return [
        { key: 'placeName', label: this.loc('AdminPanel_Location'), width: '1.35fr' },
        { key: 'coords', label: this.loc('AdminPanel_Coordinates'), width: '1.9fr' },
        { key: 'dimension', label: this.loc('AdminPanel_Dimension'), width: '.5fr' },
        { key: 'action', label: '', width: '1.05fr' }
      ]
    }
  },
  mounted: function () {
    this.requestData()
  },
  methods: {
    ...mapMutations('reportMenu', ['startTeleportsRequest']),
    requestData: function () {
      this.startTeleportsRequest()
      window.mp.triggerServer('admin:teleports:request')
    },
    go: function (item) {
      window.mp.triggerServer('admin:teleport:go', JSON.stringify(item))
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
.pill,
.action-btn {
  border: 1px solid rgba(165, 193, 255, 0.45);
  border-radius: 999px;
  background: rgba(0, 0, 0, 0.42);
  color: #fff;
  font-weight: 700;
  text-transform: uppercase;
  cursor: pointer;
}
.pill {
  height: 2.85rem;
  padding: 0 1rem;
  font-size: .78rem;
}
.action-btn {
  height: 2.35rem;
  font-size: clamp(.72rem, .78vw, .84rem);
}
.primary {
  background: rgba(62, 123, 255, 0.75);
  border-color: rgba(168, 198, 255, 0.85);
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
