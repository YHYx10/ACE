<template>
  <div class="furniture-page">
    <div class="header">
      <div class="header__info">
        <div class="heading">
          <div class="category">Artikel</div>
          <div class="title">{{ furnitureList.length }}</div>
        </div>
        <div class="heading">
          <div class="category">Nicht installiert</div>
          <div class="title">{{ furnitureNotInstalled }}</div>
        </div>
      </div>
      <div class="header__nav">
        <button class="item__btn" @click="uninstallAllFurniture">Entfernen Sie alle Möbel</button>
      </div>
    </div>
    <div class="main">
      <div
          class="billet-item"
          v-for="(item) in furnitureSortedList"
          :key="item.id"
      >
        <div class="item-text">
          <div class="title">{{ item.name }}</div>
          <div class="subtitle">${{ item.price }}</div>
        </div>
        <div v-if="!item.placed" class="item__btn" @click="installFurniture(item.name, item.id)">setzen</div>
        <div v-else class="item__btn" @click="uninstallFurniture(item.id)">setzen weg</div>
      </div>
    </div>
  </div>
</template>

<script>
import {mapGetters, mapMutations, mapState} from 'vuex'

export default {
  name: 'FurniturePage',

  components: {},

  computed: {
    ...mapState('homeMenu', ['furnitureList', 'houseId']),
    ...mapGetters('localization', ['loc']),
    furnitureNotInstalled: function () {
      return this.furnitureList.filter(item => item.placed === false).length
    },
    furnitureSortedList: function () {
      return this.furnitureList.slice().sort(function (a, b) {
        if (a.placed && !b.placed) {
          return 1;
        }
        if (!a.placed && b.placed) {
          return -1;
        }
        return 0;
      });
    }
  },

  methods: {
    ...mapMutations('homeMenu', ['setCurrentPage', 'uninstallFurniture', 'uninstallAllFurniture']),
    installFurniture: function (name, id) {
      window.mp.trigger('homeMenu:installFurniture', this.houseId, id)
    }
  }
}
</script>

<style lang="scss" scoped>
.furniture-page {
  .item-text {
    .title {
      width: 10rem;
    }
    .subtitle {
      margin-left: 4rem;
    }
  }
}
</style>
