<template>
  <div class="result-block">
    <div class="title">{{ loc('fam:btl:res:cond') }}</div>
    <div v-if="currentBuiss" class="block">
      <div class="subtitle">
        {{ loc('fam:btl:res:targ') }}
      </div>
      <div class="row">
        <img src="/img/familyMenu/eventsPage/owner.png" alt="" />
        <TitleItem
          :name="'Owner'"
          :value="[
            businessList.find((element) => element.id === currentBuiss)
              .famOwner,
          ]"
          :fontSize="32"
        />
      </div>
      <div class="row">
        <img src="/img/familyMenu/eventsPage/money.png" alt="" />
        <TitleItem
          :name="'income per hour'"
          :value="[
            `$${businessList
              .find((element) => element.id === currentBuiss)
              .income.toLocaleString('en-US')}`,
          ]"
          :fontSize="32"
        />
      </div>
    </div>
    <div v-if="currentPlace" class="block">
      <div class="subtitle">
        {{ loc('fam:btl:res:place') }}
      </div>
      <div class="row">
        <img src="/img/familyMenu/eventsPage/map.png" alt="" />
        <TitleItem
          :name="''"
          :value="[
            loc(placesList.find((element) => element.id === currentPlace).name),
          ]"
          :fontSize="32"
        />
      </div>
    </div>
    <div v-if="currentWeapon" class="block">
      <div class="subtitle">
        {{ loc('fam:btl:res:weap') }}
      </div>
      <div class="row">
        <img src="/img/familyMenu/eventsPage/gun.png" alt="" />
        <TitleItem
          :name="''"
          :value="[
            loc(
              weaponsList.find((element) => element.id === currentWeapon).name
            ),
          ]"
          :fontSize="32"
        />
      </div>
    </div>
    <template v-if="currentPlace && currentBuiss && currentWeapon">
      <div class="time-block">
        <div class="input-date">
          <div class="subtitle">
         date
</div>
          <input type="date" class="date" v-model="date" />
        </div>
        <div class="input-time">
          <div class="subtitle">
            time
          </div>
          <div class="line">
            <input
              type="number"
              class="time"
              v-model="timeHour"
              @input="onChangeHours"
            />
            :
            <input
              type="number"
              class="time"
              v-model="timeMinute"
              @input="onChangeMinutes"
            />
          </div>
        </div>
      </div>
      <div class="title-comment">{{ loc('fam:btl:res:comment') }}</div>
      <textarea placeholder="Enter a comment.." v-model="text"></textarea>
      <DefaultBtn @click="pushRegBattle">declare a battle</DefaultBtn>
    </template>
  </div>
</template>

<script>
import { mapState, mapGetters } from 'vuex'
import TitleItem from '../../../components/TitleItem.vue'
import DefaultBtn from '../../../../UI/button/DefaultBtn'

export default {
  name: 'ResultBlock',
  props: {
    currentPlace: Number,
    currentBuiss: Number,
    currentWeapon: Number,
    setNotification: Function,
  },
  data: function() {
    return {
      date: null,
      time: '13:00',
      timeHour: '00',
      timeMinute: '00',
      text: null,
    }
  },
  methods: {
    pushRegBattle: function() {
      if (this.date && this.timeHour != '' && this.timeMinute != '') {
        //this.setNotification()
        window.mp.trigger(
          'familyMenu:pushRegBattle',
          this.currentPlace,
          this.currentBuiss,
          this.currentWeapon,
          this.date,
          `${this.timeHour}:${this.timeMinute}`,
          this.text
        )
        this.date = null
        this.timeHour = 0
        this.timeMinute = 0
        this.text = null
        this.$emit('pushRegBattle')
      }
    },
    onChangeHours: function() {
      if (this.timeHour > 23) this.timeHour = 23
      else if (this.timeHour < 0) this.timeHour = 0
      if (this.timeHour.toString().length > 2) this.timeHour = this.timeHour * 1
      if (this.timeHour.toString().length == 1)
        this.timeHour = `0${this.timeHour}`
    },
    onChangeMinutes: function() {
      if (this.timeMinute > 59) this.timeMinute = 59
      else if (this.timeMinute < 0) this.timeMinute = 0
      if (this.timeMinute.toString().length > 2)
        this.timeMinute = this.timeMinute * 1
      if (this.timeMinute.toString().length == 1)
        this.timeMinute = `0${this.timeMinute}`
    },
  },
  computed: {
    ...mapGetters('localization', ['loc']),
    ...mapState('familyMenu/eventsPage', [
      'placesList',
      'weaponsList',
      'businessList',
    ]),
  },
  components: { TitleItem, DefaultBtn },
}
</script>

<style lang="scss" scoped>
div,
span,
button,
input {
  font-family: 'Akrobat';
  font-weight: 700;

  text-transform: uppercase;
  color: #ffffff;
}
input {
  font-weight: 700;
  font-size: 1.852vh;
  text-align: center;
  color: rgba(255, 255, 255, 0.5);
  background: rgba(217, 217, 217, 0.05);
  &::-webkit-inner-spin-button,
  &::-webkit-calendar-picker-indicator,
  &::-webkit-clear-button,
  &::-webkit-datetime-edit-ampm-field {
    display: none;
    -webkit-appearance: none;
    -moz-appearance: none;
    -o-appearance: none;
    -ms-appearance: none;
    appearance: none;
  }
}
.result-block {
  display: flex;
  margin-top: -12.037vh;
  flex-flow: column;
  height: 81.204vh;
  width: 31.111vh;
  margin-left: auto;
  margin-right: 5.926vh;
  .title {
    font-size: 2.963vh;
    line-height: 3.704vh;
    margin-bottom: 2.315vh;
  }
  .row {
    img {
      width: 4.63vh;
      height: 4.63vh;
    }
    display: flex;
    gap: 1.111vh;
    align-items: center;
  }

  .block {
    display: flex;
    flex-direction: column;
    gap: 1.852vh;
    .subtitle {
      font-size: 1.852vh;
      line-height: 2.315vh;
      margin-bottom: -0.278vh;
      color: rgba(255, 255, 255, 0.5);
    }
    margin-bottom: 2.407vh;
  }

  .time-block {
    display: flex;
    gap: 1.852vh;
    margin-bottom: 3.519vh;
    .subtitle {
      margin-left: 0.463vh;
      font-size: 1.852vh;
      line-height: 2.315vh;
      margin-bottom: 1.481vh;
      color: rgba(255, 255, 255, 0.5);
    }

    .input-date {
      input {
        text-transform: uppercase;
        width: 16.852vh;
        height: 4.444vh;
      }
    }
    .input-time {
      .line {
        display: flex;
        align-items: center;
        gap: 0.37vh;
        input {
          width: 4.444vh;
          height: 4.444vh;
        }
      }
    }
  }

  .title-comment {
    margin-bottom: 1.667vh;
    font-size: 1.667vh;
    line-height: 2.037vh;
  }

  textarea {
    margin-bottom: 1.852vh;
    width: 28.704vh;
    height: 9.074vh;
    background-color: transparent;
    border: 0.093vh solid rgba(255, 255, 255, 0.2);
    font-family: Akrobat;
    padding: 0.6rem 0.8rem;
    resize: none;
    outline: none;
    text-transform: uppercase;
    font-weight: 700;
    font-size: 1.296vh;
    line-height: 1.574vh;

    color: rgba(255, 255, 255, 0.5);
    &::-webkit-scrollbar {
      width: 0;
      background-color: transparent;
    }
  }
  button {
    width: 28.704vh;
    height: 6.944vh;
    font-family: 'Akrobat';
  }
}
</style>
