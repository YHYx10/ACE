<template>
  <div class="animations-list">
    <div class="content">
      <Card 
        v-for="item in currentAnimations.array"
        :key="item.id"
        :title="loc(item.name)"
        :buttonText="''"
        :imgPath="'https://local.mikzzz.ru/animations/items/' + item.key + '.gif'"
        :isBtnActive="item.isFavorite"
        @onBtnClick="setFavorite(item)"
        @click="$emit('onSelect', item)"
      />
    </div>
  </div>
</template>

<script>
import { mapMutations, mapGetters, mapState } from 'vuex'
import Card from './Card.vue'

export default {
  name: 'AnimationList',
  props: {
    currentAnimations: Object,
  },
  computed: {
    ...mapGetters('localization', ['loc']),
    ...mapState('animationsMenu', ['currentKey']),
  },
  methods: {
    ...mapMutations('animationsMenu', ['setFavorite', 'saveAnim']),
  },
  components: { Card },
}
</script>

<style lang="scss" scoped>
.animations-list {
  width: 76vw;
  height: 77.13vh;
  overflow-y: scroll;
  .content{
    display: inline-flex;
    flex-wrap: wrap;
    gap: 3.704vh;
    padding: 0.093vh;
    .card {
      width: 23.148vh;
      height: 23.148vh
    }
    
  }

  &::-webkit-scrollbar {
    display: none;
  }
}
</style>
