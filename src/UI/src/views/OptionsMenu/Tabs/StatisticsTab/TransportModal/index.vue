<template>
  <div class="modal">
    <close-button
      :styles="{ position: 'absolute', top: '0', right: '0' }"
      @close="$emit('close')"
    />
    <div class="modal__side">
      <div class="heading">
        <div class="category">Liste von Ihnen</div>
        <div class="title">Autos</div>
      </div>
      <div class="list">
        <list-item
          v-for="(item, index) in property.transport"
          :key="index"
          :item="item"
          :currentId="currentCar.id"
          @set-current-car="setCurrentCar"
        />
      </div>
    </div>
    <div class="modal__main">
      <div class="heading">
        <div class="category">Abschnitt</div>
        <div class="title">Auto´s</div>
        <div class="heading-info">
          <div class="category">{{ currentCar.numbers }}</div>
          <div class="title">{{ currentCar.name }}</div>
        </div>
      </div>
      <div class="actions">
        <actions-item
          v-for="item in actionsItems"
          :key="item.id"
          :item="item"
          :currentCarId="currentCar.id"
          @onClick="onAction"
        />
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters, mapMutations } from 'vuex'
import ListItem from './common/ListItem'
import ActionsItem from './common/ActionsItem'
import CloseButton from '../../../common/CloseButton.vue'

export default {
  name: 'TransportModal',

  components: {
    ListItem,
    ActionsItem,
    CloseButton,
  },

  data: function() {
    return {
      currentCar: null,
    }
  },

  computed: {
    ...mapState('optionsMenu', ['property']),
    ...mapGetters('localization', ['loc']),
    actionsItems: function() {
      return [
        {
          id: 0,
          title: 'mmain:stats:veh:take',
          icon: 'car',
          event: 'mmenu:vehicle:togarage',
        },
        {
          id: 1,
          title: 'mmain:stats:veh:gkey',
          icon: 'key',
          event: 'mmenu:vehicle:makekey',
        },
        {
          id: 2,
          title: 'mmain:stats:veh:chkey',
          icon: 'key',
          event: 'mmenu:vehicle:changekey',
        },
        {
          id: 4,
          title: 'mmain:stats:veh:search',
          icon: 'find',
          event: 'vehicle::key::enableGPS',
        },
        {
          id: 5,
          title: 'mmain:stats:veh:act:sellto',
          icon: 'sell',
          event: 'mmenu:cars:sell:toplayer',
        },
      ]
    },
  },

  methods: {
    ...mapMutations('optionsMenu', ['setDialog']),
    setCurrentCar: function(item) {
      this.currentCar = item
    },
    onAction(event) {
      if (this.currentCar == null) return
      if (event === 'mmenu:cars:sell:toplayer') {
        this.setDialog({
          input: 'number',
          input2: 'number',
          callback: (val, val2) => {
            val = parseInt(val)
            val2 = parseInt(val2)
            if (isNaN(val) || isNaN(val2) || val < 1 || val2 < 1) {
              window.setData('notifyList/notify', {
                type: 1,
                position: 2,
                message: 'mmain:frac:dialog:data:wrong',
                time: 3000,
              })
            } else {
              window.mp.triggerServer(event, this.currentCar.id, val, val2)
            }
          },
          value: '',
          placeholder: 'mmain:stats:biz:pcell:pl',
          placeholder2: 'mmain:stats:biz:pcell:pl2',
          tittle: `mmain:stats:veh:act:sellto`,
          subtittle: '',
          bg: undefined,
        })
      } else window.mp.triggerServer(event, this.currentCar.id)
    },
  },

  created: function() {
    this.setCurrentCar(this.property.transport[0])
  },
}
</script>

<style lang="scss" scoped>
.modal {
  position: absolute;
  display: flex;
  top: -0.1rem;
  left: 0;
  width: 100%;
  height: 100%;
  background: transparent;
  background-blend-mode: overlay;

  &__side {
    margin: 0 2rem 0 0.7rem;

    .heading {
      margin-bottom: 1rem;
    }

    .list {
      width: 28rem;
      height: 35rem;
      overflow-y: scroll;

      scrollbar-width: thin;
      scrollbar-color: #5cff80 #444444;

      &::-webkit-scrollbar {
        display: block;
        width: 0.1rem;
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

  &__main {
    flex: 1 1 100%;
    .heading {
      &-info {
        margin-top: 1rem;
        padding: 1rem 2rem;
        border: 1px solid;
        border-right: none;
        border-bottom: none;
        border-image: linear-gradient(
            90deg,
            rgba(255, 255, 255, 0.1) 30%,
            rgba(0, 0, 0, 0) 45%
          )
          1;
        background: linear-gradient(
          90deg,
          rgba(255, 255, 255, 0.03) 0%,
          rgba(0, 0, 0, 0) 45%
        );
        .category {
          font-size: 1rem;
          color: #5cff80;
        }
        .title {
          font-size: 1.9rem;
        }
      }
    }

    .actions {
      margin-top: 2rem;
      display: grid;
      grid-template-columns: repeat(3, 1fr);
      grid-gap: 0.5rem;
    }
  }
}
</style>
