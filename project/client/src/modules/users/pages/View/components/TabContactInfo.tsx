// source
import { ItemUserDto } from 'src/models/auth/usersDto';

interface ContactInfoProps {
    user: ItemUserDto;
}

const TabContactInfo = ({user}: ContactInfoProps) => {
  return (
    <div
        className="p-3 border rounded bg-light"
        style={{ flex: 1, overflowY: "auto" }}
    >
        <div className="mt-2">
            <strong className="strong-margin-right">
                <i className="bi bi-geo-alt-fill icon-margin-right" /> Address
            </strong>
            <span>{user.address}</span>
        </div>
        <div className="mt-5">
            <strong className="strong-margin-right">
                <i className="bi bi-postcard-fill icon-margin-right"/> Zip code
            </strong>
            <span>{user.zipCode}</span>
        </div>
        <div className="mt-5">
            <strong className="strong-margin-right">
                <i className="bi bi-building-fill icon-margin-right" /> City
            </strong>
            <span>{user.city}</span>
        </div>
        <div className="mt-5">
            <strong className="strong-margin-right">
                <i className="bi bi-map-fill icon-margin-right" /> State
            </strong>
            <span>{user.state}</span>
        </div>
        <div className="mt-5">
            <strong className="strong-margin-right">
                <i className="bi bi-globe2 icon-margin-right" /> Country
            </strong>
            <span>{user.country}</span>
        </div>
        <hr />
        <div className="mt-4">
            <strong className="strong-margin-right">
                <i className="bi bi-telephone-fill icon-margin-right" /> Phone Number
            </strong>
            <span>{user.phoneNumber}</span>
        </div>
        <div className="mt-5">
            <strong className="strong-margin-right">
                <i className="bi bi-telephone icon-margin-right" /> Phone Confirmed
            </strong>
            {user.phoneNumberConfirmed ? (
            <i className="bi bi-check-square-fill text-success" />
            ) : (
            <i className="bi bi-x-square-fill text-danger" />
            )}
        </div>
        <hr />
        <div className="mt-4">
            <strong className="strong-margin-right">
                <i className="bi bi-phone-fill icon-margin-right" /> Mobile Phone Number
            </strong>
            <span>{user.mobilePhoneNumber}</span>
        </div>
        <div className="mt-5">
            <strong className="strong-margin-right">
                <i className="bi bi-phone icon-margin-right" /> Mobile Phone Confirmed
            </strong>
            {user.mobilePhoneNumberConfirmed ? (
            <i className="bi bi-check-square-fill text-success" />
            ) : (
            <i className="bi bi-x-square-fill text-danger" />
            )}
        </div>
    </div>
  )
}

export default TabContactInfo