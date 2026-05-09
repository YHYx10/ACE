<template>
  <div class="admin-page generic-table-page">
    <div class="page-header">
      <div>
        <div class="page-title">{{ loc('AdminPanel_Murders') }}</div>
        <div class="page-subtitle">{{ loc('AdminPanel_MurdersSubtitle') }}</div>
      </div>
      <button class="pill" @click="requestData">{{ loc('AdminPanel_Refresh') }}</button>
    </div>

    <AdminTableShell
      :columns="columns"
      :items="murders"
      :loading="murdersLoading"
      :loading-text="loc('AdminPanel_Loading')"
      :empty-text="loc('AdminPanel_NoMurdersFound')"
    >
      <template #default="{ item }">
        <div class="cell strong">{{ item.killer }}</div>
        <div class="cell">{{ item.target }}</div>
        <div class="cell muted">{{ item.weapon }}</div>
        <div class="cell id">{{ item.distance }}</div>
        <div class="cell">{{ formatDate(item.date) }}</div>
      </template>
    </AdminTableShell>
  </div>
</template>

<script>
import { mapGetters, mapMutations, mapState } from 'vuex'
import AdminTableShell from '../components/AdminTableShell'

export default {
  name: 'Murders',
  components: { AdminTableShell },
  computed: {
    ...mapState('reportMenu', ['murders', 'murdersLoading']),
    ...mapGetters('localization', ['loc']),
    columns: function () {
      return [
        { key: 'killer', label: this.loc('AdminPanel_Killer'), width: '1.15fr' },
        { key: 'target', label: this.loc('AdminPanel_Victim'), width: '1.15fr' },
        { key: 'weapon', label: this.loc('AdminPanel_Weapon'), width: '1.15fr' },
        { key: 'distance', label: this.loc('AdminPanel_Distance'), width: '.55fr' },
        { key: 'date', label: this.loc('AdminPanel_DateTime'), width: '1.15fr' }
      ]
    }
  },
  mounted: function () {
    this.requestData()
  },
  methods: {
    ...mapMutations('reportMenu', ['startMurdersRequest']),
    requestData: function () {
      this.startMurdersRequest()
      window.mp.triggerServer('admin:murders:request')
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
