<template>
  <div class="choice place-choice">
    <HeadItem
      :title="loc('fam:btl:choice:targ')"
      :text="loc('fam:btl:choice:biz')"
    />
    <div class="place-list">
      <PlaceItem
        v-for="item in getPlaceList"
        :key="item.key"
        :img="`/img/familyMenu/eventsPage/places/${item.img}.png`"
        :text="loc(item.name)"
        :selected="item.id === currentPlace"
        @onSelect="setCurrentPlace(item.id)"
      />
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from 'vuex'
import HeadItem from '../../components/HeadItem.vue'
import PlaceItem from '../../components/PlaceItem.vue'
export default {
  name: 'PlaceChoice',
  props: {
    setCurrentPlace: Function,
    currentPlace: Number,
  },
  computed: {
    ...mapGetters('localization', ['loc']),
    ...mapState('familyMenu/eventsPage', ['placesList']),
    getPlaceList() {
      let result = this.placesList.slice().filter((item) => item.active)
      return result
    },
  },
  components: { HeadItem, PlaceItem },
}
</script>

<style lang="scss" scoped>
.place-choice {
  display: flex;
  flex-flow: column;
  margin-left: 7.87vh;
  .place-list {
    display: grid;
    margin-top: 3.148vh;
    width: 73.241vh;
    overflow-y: auto;
    grid-template-columns: repeat(3, 1fr);
    gap: 0.926vh;
    &::-webkit-scrollbar {
      background-color: transparent;
      width: 0.296vh;
      &-thumb {
        background: rgba(255, 255, 255, 0.1);
        border-radius: 0.148vh;
      }
    }
    
  }
}
</style>
