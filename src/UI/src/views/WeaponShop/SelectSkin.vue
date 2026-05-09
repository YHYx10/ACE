<template>
  <div class="select-skin">
    <div :style="skins.length ? '' : 'opacity: 0;'" class="title">
 Appearance:
    </div>
    <div class="skin-list">
      <WeaponComponent
        v-for="(skin, index) in skins"
        :key="index"
        :index="index"
        :title="'Skin ' + (index + 1)"
        :name="loc(skin.Name)"
        :price="skin.Price"
        :isCurrent="current === index"
        :img="img"
        @onSelect="onSelect(index)"
      />
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import WeaponComponent from './WeaponComponent.vue'
import ComponentSlots from './ComponentSlots'

export default {
  computed: {
    ...mapGetters('localization', ['loc']),
  },
  props: {
    skins: Array,
    current: Number,
    img: String,
  },
  data() {
    return {
      ComponentSlots,
    }
  },
  components: { WeaponComponent },
  methods: {
    onSelect(index) {
      if (index === this.current) index = -1
      this.$emit('onSelect', index, ComponentSlots['Skin'], {
        slot: 'Skin',
        price: this.skins[index] ? this.skins[index].Price : 0,
      })
    },
  },
}
</script>

<style lang="scss" scoped>
.select-skin {
  width: 30.19vh;
  .title {
    font-family: 'Montserrat';
    font-style: normal;
    font-weight: 700;
    font-size: 1.85vh;
    line-height: 2.22vh;
    color: #ffffff;
    margin-bottom: 4.63vh;
  }
  .skin-list {
    width: min-content;
    height: 43.52vh;
    overflow-y: auto;

    .weapon-component {
      margin: 0.74vh 0;
    }
    &::-webkit-scrollbar {
      display: none;
      width: 0.28vh;
    }
    &::-webkit-scrollbar-track {
      background: rgba(255, 255, 255, 0.23);
    }
    &::-webkit-scrollbar-thumb {
      background: #ff0000;
    }
  }
}
</style>
