<template>
  <div class="admin-page generic-table-page">
    <div class="page-header">
      <div>
        <div class="page-title">{{ loc('AdminPanel_Organizations') }}</div>
        <div class="page-subtitle">{{ loc('AdminPanel_OrganizationsSubtitle') }}</div>
      </div>
      <button class="pill" @click="requestData">{{ loc('AdminPanel_Refresh') }}</button>
    </div>

    <AdminTableShell
      :columns="columns"
      :items="organizations"
      :loading="organizationsLoading"
      :error-text="organizationsError"
      :loading-text="loc('AdminPanel_Loading')"
      :empty-text="loc('AdminPanel_NoOrganizationsFound')"
    >
      <template #default="{ item }">
        <div class="cell id">{{ item.id }}</div>
        <div class="cell strong">{{ item.orgName }}</div>
        <div class="cell">{{ formatMoney(item.balance) }}</div>
        <div class="cell muted">{{ item.fuelLeft }}</div>
        <div class="cell muted">{{ item.fuelLimit }}</div>
      </template>
    </AdminTableShell>
  </div>
</template>

<script>
import { mapGetters, mapMutations, mapState } from 'vuex'
import AdminTableShell from '../components/AdminTableShell'

export default {
  name: 'Organizations',
  components: { AdminTableShell },
  data: function () {
    return {
      requestTimer: null
    }
  },
  computed: {
    ...mapState('reportMenu', ['organizations', 'organizationsLoading', 'organizationsError']),
    ...mapGetters('localization', ['loc']),
    columns: function () {
      return [
        { key: 'id', label: this.loc('AdminPanel_Id'), width: '.45fr' },
        { key: 'orgName', label: this.loc('AdminPanel_Organization'), width: '1.4fr' },
        { key: 'balance', label: this.loc('AdminPanel_Balance'), width: '1fr' },
        { key: 'fuelLeft', label: 'Fuel left', width: '.9fr' },
        { key: 'fuelLimit', label: 'Fuel limit', width: '.9fr' }
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
    ...mapMutations('reportMenu', ['startOrganizationsRequest', 'setOrganizationsError']),
    clearRequestTimer: function () {
      if (!this.requestTimer) return
      clearTimeout(this.requestTimer)
      this.requestTimer = null
    },
    startRequestFallback: function () {
      this.clearRequestTimer()
      this.requestTimer = setTimeout(() => {
        if (this.organizationsLoading) {
          this.setOrganizationsError('No response from server while loading organizations.')
        }
      }, 8000)
    },
    requestData: function () {
      this.startOrganizationsRequest()
      this.startRequestFallback()
      if (window.mp && typeof window.mp.triggerServer === 'function') {
        window.mp.triggerServer('admin:orgs:request')
      } else {
        this.setOrganizationsError('Admin bridge is not available.')
      }
    },
    formatMoney: function (value) {
      return `$${Number(value || 0).toLocaleString('en-US')}`
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
