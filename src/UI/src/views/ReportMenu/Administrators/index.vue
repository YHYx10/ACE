<template>
  <div class="admin-page administrators">
    <div class="page-header">
      <div>
        <div class="page-title">{{ loc('AdminPanel_Administrators') }}</div>
        <div class="page-subtitle">{{ loc('AdminPanel_AdministratorsSubtitle') }}</div>
      </div>
      <button class="pill" @click="requestData">{{ loc('AdminPanel_Refresh') }}</button>
    </div>

    <AdminTableShell
      :columns="columns"
      :items="administrators"
      :loading="administratorsLoading"
      :error-text="administratorsError"
      :loading-text="loc('AdminPanel_Loading')"
      :empty-text="loc('AdminPanel_NoAdministratorsFound')"
    >
      <template #default="{ item }">
        <div class="cell status" :class="item.status.toLowerCase()">{{ item.status }}</div>
        <div class="cell strong">{{ item.playerName }}</div>
        <div class="cell id">{{ item.playerId }}</div>
        <div class="cell muted">{{ item.adminRankName }}</div>
        <div class="cell">{{ item.lastSeen }}</div>
        <button class="action-btn primary" @click="openRankModal(item)">{{ loc('AdminPanel_IssueRank') }}</button>
      </template>
    </AdminTableShell>

    <div class="rank-modal" v-if="selectedAdmin">
      <div class="rank-modal__card">
        <div class="rank-modal__title">{{ loc('AdminPanel_IssueRank') }}</div>
        <div class="rank-modal__subtitle">{{ selectedAdmin.playerName }} [{{ selectedAdmin.playerId }}]</div>
        <input type="number" min="1" v-model.number="nextRank" class="rank-modal__input">
        <div class="rank-modal__actions">
          <button class="pill primary" @click="submitRank">{{ loc('AdminPanel_Save') }}</button>
          <button class="pill" @click="closeRankModal">{{ loc('AdminPanel_Cancel') }}</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapMutations, mapState } from 'vuex'
import AdminTableShell from '../components/AdminTableShell'

export default {
  name: 'Administrators',
  components: { AdminTableShell },
  data: function () {
    return {
      selectedAdmin: null,
      nextRank: 1,
      requestTimer: null
    }
  },
  computed: {
    ...mapState('reportMenu', ['administrators', 'administratorsLoading', 'administratorsError']),
    ...mapGetters('localization', ['loc']),
    columns: function () {
      return [
        { key: 'status', label: this.loc('AdminPanel_Status'), width: '.8fr' },
        { key: 'playerName', label: this.loc('AdminPanel_Name'), width: '1.25fr' },
        { key: 'playerId', label: this.loc('AdminPanel_Id'), width: '.45fr' },
        { key: 'adminRankName', label: this.loc('AdminPanel_Rank'), width: '1.35fr' },
        { key: 'lastSeen', label: this.loc('AdminPanel_LastSeen'), width: '.9fr' },
        { key: 'action', label: '', width: '1.05fr' }
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
    ...mapMutations('reportMenu', ['startAdministratorsRequest', 'setAdministratorsError']),
    clearRequestTimer: function () {
      if (!this.requestTimer) return
      clearTimeout(this.requestTimer)
      this.requestTimer = null
    },
    startRequestFallback: function () {
      this.clearRequestTimer()
      this.requestTimer = setTimeout(() => {
        if (this.administratorsLoading) {
          this.setAdministratorsError('No response from server while loading administrators.')
        }
      }, 8000)
    },
    requestData: function () {
      this.startAdministratorsRequest()
      this.startRequestFallback()
      if (window.mp && typeof window.mp.triggerServer === 'function') {
        window.mp.triggerServer('admin:administrators:request')
      } else {
        this.setAdministratorsError('Admin bridge is not available.')
      }
    },
    openRankModal: function (item) {
      this.selectedAdmin = item
      this.nextRank = item.adminLevel || 1
    },
    closeRankModal: function () {
      this.selectedAdmin = null
      this.nextRank = 1
    },
    submitRank: function () {
      if (!this.selectedAdmin) return
      window.mp.triggerServer('admin:administrator:rank', JSON.stringify({
        playerId: this.selectedAdmin.playerId,
        rank: Number(this.nextRank)
      }))
      this.closeRankModal()
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
  &.status {
    font-weight: 700;
    &.online {
      color: #d7ff3f;
    }
    &.offline {
      color: rgba(255, 105, 105, 0.86);
    }
  }
}
.rank-modal {
  position: absolute;
  inset: 0;
  background: rgba(0, 0, 0, 0.38);
  display: flex;
  align-items: center;
  justify-content: center;
  &__card {
    width: 21rem;
    padding: 1.4rem;
    border: 1px solid rgba(255, 255, 255, 0.12);
    border-radius: 1rem;
    background: rgba(5, 10, 20, 0.94);
  }
  &__title {
    font-size: 1.5rem;
    font-weight: 700;
  }
  &__subtitle {
    margin-top: .3rem;
    color: rgba(255, 255, 255, 0.58);
    font-size: .88rem;
  }
  &__input {
    width: 100%;
    height: 3rem;
    margin-top: 1rem;
    border: none;
    outline: none;
    border-radius: .85rem;
    background: rgba(0, 0, 0, 0.5);
    color: #fff;
    padding: 0 1rem;
    font-size: 1rem;
  }
  &__actions {
    display: flex;
    justify-content: flex-end;
    gap: .5rem;
    margin-top: 1rem;
  }
}
</style>
