<template>
  <div class="admin-table-shell">
    <div class="admin-table-shell__head" :style="gridStyle">
      <div
        v-for="column in columns"
        :key="column.key"
        :class="['admin-table-shell__head-cell', column.className]"
      >
        {{ column.label }}
      </div>
    </div>
    <div class="admin-table-shell__body scroll">
      <div class="admin-table-shell__state" v-if="loading">{{ loadingText }}</div>
      <div class="admin-table-shell__state error" v-else-if="errorText">{{ errorText }}</div>
      <div class="admin-table-shell__state" v-else-if="!items.length">{{ emptyText }}</div>
      <template v-else>
        <div
          v-for="item in items"
          :key="item[rowKey] || item.id || item.key"
          class="admin-table-shell__row"
          :style="gridStyle"
        >
          <slot :item="item"></slot>
        </div>
      </template>
    </div>
  </div>
</template>

<script>
export default {
  name: 'AdminTableShell',
  props: {
    columns: {
      type: Array,
      default: function () {
        return []
      }
    },
    items: {
      type: Array,
      default: function () {
        return []
      }
    },
    rowKey: {
      type: String,
      default: 'id'
    },
    loading: {
      type: Boolean,
      default: false
    },
    loadingText: {
      type: String,
      default: 'Loading...'
    },
    emptyText: {
      type: String,
      default: 'No data'
    },
    errorText: {
      type: String,
      default: ''
    }
  },
  computed: {
    gridStyle: function () {
      return {
        gridTemplateColumns: this.columns.map((column) => column.width || '1fr').join(' ')
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.admin-table-shell {
  width: 100%;
  height: 100%;
  display: flex;
  flex-flow: column;
  &__head,
  &__row {
    display: grid;
    align-items: center;
    gap: .7vw;
  }
  &__head {
    min-height: 2.5rem;
    padding: 0 .4vw .8vh;
    color: rgba(255, 255, 255, 0.68);
    border-bottom: 1px solid rgba(255, 255, 255, 0.18);
    font-size: clamp(.76rem, .82vw, .9rem);
  }
  &__body {
    flex: 1;
    overflow-y: auto;
    padding-right: .35vw;
  }
  &__row {
    min-height: 5.1vh;
    padding: .2rem .4vw;
    border-bottom: 1px solid rgba(255, 255, 255, 0.07);
    color: rgba(255, 255, 255, 0.9);
    font-size: clamp(.88rem, .96vw, 1rem);
    transition: background .16s ease, box-shadow .16s ease;
    &:hover {
      background: rgba(255, 255, 255, 0.055);
      box-shadow: inset 0 0 1.4rem rgba(190, 220, 255, 0.045);
    }
  }
  &__state {
    padding: 2rem .4vw;
    color: rgba(255, 255, 255, 0.6);
    font-size: clamp(1rem, 1.2vw, 1.2rem);
    &.error {
      color: rgba(255, 120, 120, 0.9);
    }
  }
}
</style>
