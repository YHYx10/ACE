<template>
  <div class="admin-page generic-table-page">
    <div class="page-header">
      <div>
        <div class="page-title">{{ loc('AdminPanel_Families') }}</div>
        <div class="page-subtitle">{{ loc('AdminPanel_FamiliesSubtitle') }}</div>
      </div>
      <button class="pill" @click="requestData">{{ loc('AdminPanel_Refresh') }}</button>
    </div>

    <AdminTableShell
      :columns="columns"
      :items="families"
      :loading="familiesLoading"
      :error-text="familiesError"
      :loading-text="loc('AdminPanel_Loading')"
      :empty-text="loc('AdminPanel_NoFamiliesFound')"
    >
      <template #default="{ item }">
        <div class="cell strong">{{ item.familyName }}</div>
        <div class="cell">{{ item.leaderName }}</div>
        <div class="cell id">{{ item.leaderId }}</div>
        <div class="cell">{{ formatMoney(item.bank) }}</div>
        <div class="cell muted">#{{ item.houseNumber }}</div>
        <div class="cell status">{{ item.status }}</div>
        <button class="action-btn" @click="joinFamily(item)">{{ loc('AdminPanel_JoinFamily') }}</button>
        <button class="action-btn primary" @click="tpHouse(item)">{{ loc('AdminPanel_TpToHouse') }}</button>
      </template>
    </AdminTableShell>
  </div>
</template>

<script>
import { mapGetters, mapMutations, mapState } from 'vuex'
import AdminTableShell from '../components/AdminTableShell'

export default {
  name: 'Families',
  components: { AdminTableShell },
  data: function () {
    return {
      requestTimer: null
    }
  },
  computed: {
    ...mapState('reportMenu', ['families', 'familiesLoading', 'familiesError']),
    ...mapGetters('localization', ['loc']),
    columns: function () {
      return [
        { key: 'familyName', label: this.loc('AdminPanel_Family'), width: '1.35fr' },
        { key: 'leaderName', label: this.loc('AdminPanel_Leader'), width: '1.2fr' },
        { key: 'leaderId', label: this.loc('AdminPanel_Id'), width: '.45fr' },
        { key: 'bank', label: this.loc('AdminPanel_Balance'), width: '.8fr' },
        { key: 'houseNumber', label: this.loc('AdminPanel_House'), width: '.55fr' },
        { key: 'status', label: this.loc('AdminPanel_Status'), width: '.8fr' },
        { key: 'join', label: '', width: '.9fr' },
        { key: 'tp', label: '', width: '1fr' }
      ]
    }
  },
  mounted: function () {
    this.requestData()
  },
  beforeDestroy: function () {
    this.clearRequestTimer()
  },
  methods: {
    ...mapMutations('reportMenu', ['startFamiliesRequest', 'setFamiliesError']),
    clearRequestTimer: function () {
      if (!this.requestTimer) return
      clearTimeout(this.requestTimer)
      this.requestTimer = null
    },
    startRequestFallback: function () {
      this.clearRequestTimer()
      this.requestTimer = setTimeout(() => {
        if (this.familiesLoading) {
          this.setFamiliesError('No response from server while loading families.')
        }
      }, 8000)
    },
    requestData: function () {
      this.startFamiliesRequest()
      this.startRequestFallback()
      if (window.mp && typeof window.mp.triggerServer === 'function') {
        window.mp.triggerServer('admin:families:request')
      } else {
        this.setFamiliesError('Admin bridge is not available.')
      }
    },
    formatMoney: function (value) {
      return `$${Number(value || 0).toLocaleString('en-US')}`
    },
    joinFamily: function (item) {
      window.mp.triggerServer('admin:family:join', JSON.stringify({ familyId: item.id }))
    },
    tpHouse: function (item) {
      window.mp.triggerServer('admin:family:tp-house', JSON.stringify({ familyId: item.id }))
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
  &.id,
  &.status {
    color: rgba(255, 255, 255, 0.62);
    font-weight: 700;
    font-size: clamp(1.02rem, 1.12vw, 1.2rem);
  }
  &.status {
    font-size: clamp(.78rem, .84vw, .92rem);
  }
}
</style>
