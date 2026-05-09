<template>
  <div class="section">
    <div class="section__main">
      <works-item
        v-for="(item, index) in worksList"
        :key="index"
        :item="item"
      />
    </div>
  </div>
</template>

<script>
import WorksItem from './common/WorksItem'
import worksList from './worksList'
import { mapGetters } from 'vuex'

export default {
  name: 'WorksSection',

  components: {
    WorksItem
  },
  computed:{
    ...mapGetters('localization', ['loc'])
  },

  data: function() {
    return {
      worksList: worksList
    }
  }
}
</script>

<style lang="scss" scoped>
.section {
  display: flex;
  flex-direction: column;
  &__main {
    display: flex;
    flex-direction: column;
    height: 35.8rem;
    padding: 0 1.7rem 0 0;
    overflow-y: scroll;
    scrollbar-width: thin;
    scrollbar-color: #5cff80 #444444;
    &::-webkit-scrollbar {
      display: block;
      width: 0.11rem;
      height: 0;
    }
    &::-webkit-scrollbar-track {
      background: #444444;
    }
    &::-webkit-scrollbar-thumb {
      background-color: #5cff80;
    }
  }
}
</style>
