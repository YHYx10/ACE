<template>
  <div class="admin-page player-penalties">
    <div class="search-overlay" v-if="!hasResultsView">
      <div class="search-overlay__title">{{ loc('AdminPanel_PlayerPenalties') }}</div>
      <div class="search-overlay__subtitle">{{ loc('AdminPanel_PenaltiesSubtitle') }}</div>
      <input
        v-model.trim="query"
        class="search-overlay__input"
        :placeholder="loc('AdminPanel_PenaltiesPlaceholder')"
        @keydown.enter="search"
      >
      <div class="search-overlay__actions">
        <button class="cta primary" @click="search">{{ loc('AdminPanel_Search') }}</button>
        <button class="cta" @click="resetSearch">{{ loc('AdminPanel_Cancel') }}</button>
      </div>
    </div>

    <div class="page-view" v-else>
      <div class="page-view__header">
        <div>
          <div class="page-view__title">{{ loc('AdminPanel_PlayerPenalties') }}</div>
          <div class="page-view__subtitle">{{ penaltiesMeta }}</div>
        </div>
        <div class="page-view__controls">
          <input
            v-model.trim="query"
            class="inline-search"
            :placeholder="loc('AdminPanel_PenaltiesPlaceholder')"
            @keydown.enter="search"
          >
          <button class="pill primary" @click="search">{{ loc('AdminPanel_Search') }}</button>
          <button class="pill" @click="resetSearch">{{ loc('AdminPanel_Reset') }}</button>
        </div>
      </div>

      <AdminTableShell
        :columns="columns"
        :items="penalties"
        :loading="penaltiesLoading"
        :loading-text="loc('AdminPanel_Loading')"
        :empty-text="loc('AdminPanel_NoPenaltiesFound')"
      >
        <template #default="{ item }">
          <div class="cell date danger">{{ item.date }}</div>
          <div class="cell strong">{{ item.playerName }}</div>
          <div class="cell id">{{ item.playerId }}</div>
          <div class="cell">{{ item.reason }}</div>
          <div class="cell muted">{{ item.punishment }}</div>
          <div class="cell strong">{{ item.adminName }}</div>
        </template>
      </AdminTableShell>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapMutations, mapState } from 'vuex'
import AdminTableShell from '../components/AdminTableShell'

export default {
  name: 'PlayerPenalties',
  components: { AdminTableShell },
  data: function () {
    return {
      query: ''
    }
  },
  computed: {
    ...mapState('reportMenu', ['penalties', 'penaltiesLoading', 'penaltiesSearched']),
    ...mapGetters('localization', ['loc']),
    columns: function () {
      return [
        { key: 'date', label: this.loc('AdminPanel_Date'), width: '1fr' },
        { key: 'playerName', label: this.loc('AdminPanel_Name'), width: '1.4fr' },
        { key: 'playerId', label: this.loc('AdminPanel_Id'), width: '.45fr' },
        { key: 'reason', label: this.loc('AdminPanel_Reason'), width: '1.45fr' },
        { key: 'punishment', label: this.loc('AdminPanel_Punishment'), width: '1.5fr' },
        { key: 'adminName', label: this.loc('AdminPanel_Admin'), width: '1.15fr' }
      ]
    },
    hasResultsView: function () {
      return this.penaltiesSearched || this.penaltiesLoading
    },
    penaltiesMeta: function () {
      if (this.penaltiesLoading) return this.loc('AdminPanel_Loading')
      return this.loc('AdminPanel_FoundRows').replace('{count}', this.penalties.length)
    }
  },
  methods: {
    ...mapMutations('reportMenu', ['startPenaltiesSearch', 'resetPenalties']),
    search: function () {
      if (!this.query) return
      this.startPenaltiesSearch(this.query)
      window.mp.triggerServer('admin:penalties:search', JSON.stringify({ query: this.query }))
    },
    resetSearch: function () {
      this.query = ''
      this.resetPenalties()
    }
  }
}
</script>

<style lang="scss" scoped>
.player-penalties {
  width: 100%;
  height: 100%;
  padding: 2.3vh 1.2vw 1.3vh;
}
.search-overlay,
.page-view {
  width: 100%;
  height: 100%;
}
.search-overlay {
  display: flex;
  flex-flow: column;
  align-items: center;
  justify-content: center;
  text-align: center;
  &__title {
    font-size: clamp(2.5rem, 3vw, 3.35rem);
    font-weight: 700;
  }
  &__subtitle {
    margin-top: .8rem;
    max-width: 32rem;
    color: rgba(255, 255, 255, 0.7);
    font-size: clamp(1.1rem, 1.3vw, 1.45rem);
    line-height: 1.15;
  }
  &__input {
    width: min(23rem, 100%);
    height: 4.9rem;
    margin-top: 1.4rem;
    border: none;
    outline: none;
    border-radius: 1.15rem;
    background: rgba(8, 7, 10, 0.7);
    color: #fff;
    text-align: center;
    font-size: clamp(1.1rem, 1.25vw, 1.35rem);
    text-transform: uppercase;
    padding: 0 1.4rem;
  }
  &__actions {
    display: flex;
    gap: .9rem;
    margin-top: 1.35rem;
  }
}
.page-view {
  display: flex;
  flex-flow: column;
  &__header {
    display: flex;
    justify-content: space-between;
    align-items: flex-end;
    gap: 1rem;
    margin-bottom: 1rem;
  }
  &__title {
    font-size: clamp(1.9rem, 2.25vw, 2.45rem);
    font-weight: 700;
  }
  &__subtitle {
    margin-top: .25rem;
    color: rgba(255, 255, 255, 0.58);
    font-size: clamp(.84rem, .94vw, 1rem);
  }
  &__controls {
    display: flex;
    align-items: center;
    gap: .55rem;
  }
}
.inline-search {
  width: 19rem;
  height: 2.85rem;
  border: 1px solid rgba(255, 255, 255, 0.08);
  border-radius: 999px;
  outline: none;
  background: rgba(0, 0, 0, 0.42);
  color: #fff;
  padding: 0 1.15rem;
  text-transform: uppercase;
  font-size: .82rem;
}
.cta,
.pill {
  border: none;
  border-radius: .7rem;
  background: rgba(8, 7, 10, 0.82);
  color: #fff;
  font-weight: 700;
  text-transform: uppercase;
  cursor: pointer;
}
.cta {
  min-width: 6.2rem;
  height: 3.4rem;
  font-size: 1rem;
}
.pill {
  height: 2.85rem;
  padding: 0 1rem;
  font-size: .78rem;
  border-radius: 999px;
}
.primary {
  background: #f0ff19;
  color: #fff;
  box-shadow: 0 0 1rem rgba(214, 255, 31, 0.28);
}
.cell {
  min-width: 0;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  &.danger {
    color: rgba(255, 73, 73, 0.88);
    font-size: clamp(.76rem, .82vw, .86rem);
  }
  &.strong {
    font-weight: 700;
  }
  &.muted {
    color: rgba(255, 255, 255, 0.65);
  }
  &.id {
    color: rgba(255, 255, 255, 0.62);
    font-weight: 700;
    font-size: clamp(1.05rem, 1.14vw, 1.25rem);
  }
}
</style>
